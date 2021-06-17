import 'package:avatar_glow/avatar_glow.dart';
import 'package:flutter/material.dart';
import 'package:recycle_market/layout/login/login.dart';
import 'package:recycle_market/layout/menu/navigation_home_screen.dart';
import 'package:recycle_market/utils/delayed_animation.dart';

class RegisterPage extends StatefulWidget {
  @override
  _RegisterPageState createState() => _RegisterPageState();
}

class _RegisterPageState extends State<RegisterPage> with SingleTickerProviderStateMixin {

  final int delayedAmount = 500;
  final email = TextEditingController();
  final password = TextEditingController();
  final confirmPassword = TextEditingController();

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
                duration: Duration(seconds: 2),
                glowColor: Colors.white24,
                repeat: true,
                repeatPauseDuration: Duration(seconds: 1),
                startDelay: Duration(seconds: 2),
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
                  "Registrate Ya!",
                  textAlign: TextAlign.center,
                  style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 35.0,
                      color: color),
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
                delay: delayedAmount + 1000,
                child: new Container(
                  height: 45,
                  width: 300,
                  child: TextField(
                    controller: confirmPassword,
                    decoration: InputDecoration(
                      hintText: 'Confirmar Contraseña',
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
                    "REGISTRAR",
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
            SizedBox(height: 50.0,),
              DelayedAnimation(
                child: FlatButton(
                  onPressed: () => {
                    Navigator.pushReplacement(
                      context,
                      MaterialPageRoute(
                        builder: (context) => LoginPage(),
                      ),
                    ),
                  },
                  child: Text(
                    "Ya Tienes Cuenta?".toUpperCase(),
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
