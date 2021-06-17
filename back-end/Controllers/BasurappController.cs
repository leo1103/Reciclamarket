using System.Threading.Tasks;
using basurapp.api.Data;
using basurapp.api.Dtos;
using basurapp.api.Helpers;
using Microsoft.AspNetCore.Mvc;
using basurapp.api.Models;
using System;


namespace basurapp.api.Controllers
{        
    [Route("api/[controller]")]
    [ApiController]

    public class BasurappController: ControllerBase
    {

        private readonly IRecyclerRepository repository;
        public BasurappController(IRecyclerRepository repo)
        {
            repository = repo;
        }
        
        [HttpPost("delivery/register")]
        public  async Task<IActionResult> registerDelivery(RegisterDeliveryDto registerDeliveryDto)
        {
            Delivery deliveryFromDto = new Delivery();
            deliveryFromDto.title=registerDeliveryDto.title;
            deliveryFromDto.description = registerDeliveryDto.description;
            deliveryFromDto.basurappUserContributor = await repository.getBasurappUserById(registerDeliveryDto.idBasurappUserContributor);
            deliveryFromDto.date = registerDeliveryDto.date;
            deliveryFromDto.latitude = registerDeliveryDto.latitude;
            deliveryFromDto.altitude = registerDeliveryDto.altitude;
            deliveryFromDto.deliveryState = 0;

            var delivery = await repository.registerDelivery(deliveryFromDto);
            await repository.saveAll(); 

            ReturnCodeDto returnCode = new ReturnCodeDto();
            if ( repository.saveAll() !=null)
            {
                returnCode.code=200;
                returnCode.data=delivery;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="request not found";
                return Ok(returnCode);                                
            }
        }

        [HttpGet("delivery/{idDelivery}")]
        public async Task<IActionResult> getDeliveryById(int idDelivery)
        {
            var request = await repository.getDeliveryById(idDelivery);
            
            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (request !=null)
            {
                returnCode.code=200;
                returnCode.data=request;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="request not found";
                return Ok(returnCode);                                
            }
        }

        [HttpGet("delivery/contributor/{idContributor}")]
        public async Task<IActionResult> getDeliveriesByContributorId([FromQuery]UserParams userParams, int idContributor)
        {
            var request = await repository.getDeliveriesByContributorId(userParams, idContributor);
            
            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (request !=null)
            {
                returnCode.code=200;
                returnCode.data=request;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="Delivery with specified contributor not found";
                return Ok(returnCode);                                
            }
        }

        [HttpGet("delivery/recolector/{idRecolector}")]
        public async Task<IActionResult> getDeliveriesByRecolectorId([FromQuery]UserParams userParams, int idRecolector)
        {
            var request = await repository.getDeliveriesByRecolectorId(userParams, idRecolector);
            
            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (request !=null)
            {
                returnCode.code=200;
                returnCode.data=request;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="Delivery with specified recolector not found";
                return Ok(returnCode);                                
            }
        }

        [HttpPut("delivery/update/{idDelivery}")]
        public async Task<IActionResult> updateDelivery(int idDelivery, [FromBody]Delivery deliveryForUpdate)
        {            
            var deliveryFromDb = await repository.getDeliveryById(idDelivery);

            deliveryFromDb.altitude = deliveryForUpdate.altitude;
            deliveryFromDb.latitude = deliveryForUpdate.latitude;
            deliveryFromDb.title = deliveryForUpdate.title;
            deliveryFromDb.description = deliveryForUpdate.description;
            deliveryFromDb.date = deliveryForUpdate.date;
            deliveryFromDb.deliveryState = deliveryForUpdate.deliveryState;

            await repository.saveAll(); 
            
            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (deliveryFromDb!=null)
            {
                returnCode.code=200;
                returnCode.data=deliveryForUpdate;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="delivery not found";
                return Ok(returnCode);                                
            }
        }

        [HttpPut("delivery/confirm/{idDelivery}/{idRecolector}")]
        public async Task<IActionResult> confirmDelivery(int idDelivery, int idRecolector)
        {            
            var deliveryFromDb = await repository.getDeliveryById(idDelivery);

            deliveryFromDb.basurappUserRecolector = await repository.getBasurappUserById(idRecolector);
            deliveryFromDb.deliveryState=1;
            await repository.saveAll(); 

            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (deliveryFromDb.basurappUserRecolector!=null)
            {
                returnCode.code=200;
                returnCode.data=deliveryFromDb;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="delivery not found";
                return Ok(returnCode);                                
            }
        }

        [HttpPut("delivery/ended/{idDelivery}")]
        public async Task<IActionResult> endDelivery(int idDelivery)
        {            
            var deliveryFromDb = await repository.getDeliveryById(idDelivery);            
            deliveryFromDb.deliveryState=2;
            await repository.saveAll(); 

            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (deliveryFromDb.basurappUserRecolector!=null)
            {
                returnCode.code=200;
                returnCode.data=deliveryFromDb;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="delivery not found";
                return Ok(returnCode);                                
            }
        }

        [HttpPut("delivery/canceled/{idDelivery}")]
        public async Task<IActionResult> cancelDelivery(int idDelivery)
        {            
            var deliveryFromDb = await repository.getDeliveryById(idDelivery);        
            deliveryFromDb.deliveryState=3;
            await repository.saveAll(); 

            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (deliveryFromDb!=null)
            {
                returnCode.code=200;
                returnCode.data=deliveryFromDb;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="delivery not found";
                return Ok(returnCode);                                
            }
        }

        [HttpPut("delivery/renew/{idDelivery}")]
        public async Task<IActionResult> renewDelivery(int idDelivery)
        {
            var deliveryFromDb = await repository.getDeliveryById(idDelivery);  
            deliveryFromDb.deliveryState=0;
            deliveryFromDb.basurappUserRecolector=null;
            await repository.saveAll(); 

            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (deliveryFromDb !=null)
            {
                returnCode.code=200;
                returnCode.data=deliveryFromDb;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="delivery not found";
                return Ok(returnCode);                                
            }
        }

        [HttpGet("delivery/free/{idRecolector}")]
        public async Task<IActionResult> getFreeDeliveries([FromQuery]UserParams userParams, int idRecolector)
        {           
            var delivery = await repository.getInProgressDeliveriesByUserID(idRecolector);

            ReturnCodeDto returnCode = new ReturnCodeDto();

            if(delivery !=null)
            {
                returnCode.code=200;
                returnCode.data=delivery;
                return Ok(returnCode);
            }

            var deliveries = await repository.getFreeDeliveries(userParams);                

            if (deliveries !=null)
            {
                Response.addPagination(deliveries.CurrentPage, deliveries.PageSize, deliveries.TotalCount, deliveries.TotalPages);
                returnCode.code=200;
                returnCode.data=deliveries;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="deliveries not found";
                return Ok(returnCode);                                
            }                                               
                           
        }

        [HttpGet("delivery/free")]
        public async Task<IActionResult> getFreeDeliveries([FromQuery]UserParams userParams)
        {           
            var deliveries = await repository.getFreeDeliveries(userParams);

            ReturnCodeDto returnCode = new ReturnCodeDto();

            if (deliveries !=null)
            {
                returnCode.code=200;
                returnCode.data=deliveries;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="deliveries not found";
                return Ok(returnCode);                                
            }                     
                           
        }

        [HttpGet("product/all")]
        public async Task<IActionResult> getProducts([FromQuery]UserParams userParams)
        {            
            var products = await repository.getProducts(userParams);                        
            Response.addPagination(products.CurrentPage, products.PageSize, products.TotalCount, products.TotalPages);

            ReturnCodeDto returnCode = new ReturnCodeDto();
            if (products !=null)
            {
                returnCode.code=200;
                returnCode.data=products;
                return Ok(returnCode);
            }
            else
            {
                returnCode.code=404;
                returnCode.data="products not found";
                return Ok(returnCode);                                
            }  
        }  

        [HttpGet("day")]
        public async Task<IActionResult> getDay([FromQuery]UserParams userParams)
        {            
            ReturnCodeDto returnCode = new ReturnCodeDto();

            returnCode.code=200;
            returnCode.data=DateTime.Now.DayOfYear;
            return Ok(DateTime.Now.DayOfYear);
        }                         
    }
}