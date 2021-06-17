import 'package:avatar_glow/avatar_glow.dart';
import 'package:flutter/material.dart';
import 'package:recycle_market/layout/menu/navigation_home_screen.dart';
import 'package:recycle_market/layout/register/register.dart';
import 'package:recycle_market/utils/delayed_animation.dart';

class LoginPage extends StatefulWidget {
  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> with SingleTickerProviderStateMixin {
  final int delayedAmount = 500;
  final email = TextEditingController();
  final password = TextEditingController();

  double _scale;
  AnimationController _controller;

  @override
  void initState() {
    _controller = AnimationController(
      vsync: this,
      duration: Duration(
        milliseconds: 100,
      ),
      lowerBound: 0.0,
      upperBound: 0.1,
    )..addListener(() {
        setState(() {});
      });
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    final color = Colors.white;
    _scale = 1 - _controller.value;

    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: Scaffold(
        backgroundColor: Colors.blue,
        body: Center(
          child: Column(
            children: <Widget>[
              AvatarGlow(
                endRadius: 90,
                duration: Duration(seconds: 1),
                glowColor: Colors.white24,
                repeat: true,
                repeatPauseDuration: Duration(seconds: 1),
                startDelay: Duration(seconds: 1),
                child: Material(
                    elevation: 8.0,
                    shape: CircleBorder(),
                    child: CircleAvatar(
                      backgroundColor: Colors.grey[100],
                      child: FlutterLogo(
                        size: 50.0,
                      ),
                      radius: 50.0,
                    )),
              ),
              DelayedAnimation(
                child: Text(
                  "Una nueva manera de reciclar",
                  textAlign: TextAlign.center,
                  style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 35.0,
                      color: color),
                ),
                delay: delayedAmount + 1000,
              ),
              SizedBox(
                height: 30.0,
              ),
              DelayedAnimation(
                child: Text(
                  "Al alcance de tus manos!",
                  style: TextStyle(fontSize: 20.0, color: color),
                ),
                delay: delayedAmount + 1000,
              ),
              SizedBox(
                height: 20,
              ),
              DelayedAnimation(
                delay: delayedAmount + 1000,
                child: new Container(
                  height: 45,
                  width: 300,
                  child: TextField(
                    controller: email,
                    decoration: InputDecoration(
                      hintText: 'Correo',
                      filled: true,
                      fillColor: Colors.white,
                      focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide(color: Colors.white),
                        borderRadius: BorderRadius.circular(25.7),
                      ),
                      enabledBorder: UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.white),
                        borderRadius: BorderRadius.circular(25.7),
                      ),
                      border: OutlineInputBorder(),
                      prefixIcon: Icon(Icons.email)
                    ),
                  ),
                ),
              ),
              SizedBox(
                height: 15,
              ),
              DelayedAnimation(
                delay: delayedAmount + 1000,
                child: new Container(
                  height: 45,
                  width: 300,
                  child: TextField(
                    controller: password,
                    decoration: InputDecoration(
                      hintText: 'Contraseña',
                      filled: true,
                      fillColor: Colors.white,
                      focusedBorder: OutlineInputBorder(
                        borderSide: BorderSide(color: Colors.white),
                        borderRadius: BorderRadius.circular(25.7),
                      ),
                      enabledBorder: UnderlineInputBorder(
                        borderSide: BorderSide(color: Colors.white),
                        borderRadius: BorderRadius.circular(25.7),
                      ),
                      border: OutlineInputBorder(),
                      prefixIcon: Icon(Icons.lock)
                    ),
                  ),
                ),
              ),
              SizedBox(
                height: 15,
              ),
              DelayedAnimation(
                child: MaterialButton(
                  child: new Text(
                    "INICIAR SESION",
                    style: TextStyle(
                      color: Colors.white,
                      fontSize: 16,
                    ),
                  ),
                  height: 45,
                  minWidth: 300,
                  color: Colors.transparent,
                  onPressed: () => {
                    Navigator.pushReplacement(
                      context,
                      MaterialPageRoute(
                        builder: (context) => NavigationHomeScreen(),
                      ),
                    ),
                  },
                ),
                delay: delayedAmount + 1000,
            ),
            SizedBox(height: 20.0,),
              DelayedAnimation(
                child: FlatButton(
                  onPressed: () => {
                    Navigator.pushReplacement(
                      context,
                      MaterialPageRoute(
                        builder: (context) => RegisterPage(),
                      ),
                    ),
                  },
                  child: Text(
                    "Registrate YA!".toUpperCase(),
                    style: TextStyle(
                        fontSize: 20.0,
                        fontWeight: FontWeight.bold,
                        color: color),
                  ),
                ),
                delay: delayedAmount + 1000,
              ),
            ],
          ),
        )
      ),
    );
  }
}
