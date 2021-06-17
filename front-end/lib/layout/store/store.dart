import 'package:flutter/material.dart';
import 'package:recycle_market/layout/menu/app_theme.dart';

class StorePage extends StatefulWidget {
  const StorePage({Key key}) : super(key: key);
  @override
  _StorePageState createState() => _StorePageState();
}

class _StorePageState extends State<StorePage> with TickerProviderStateMixin {

@override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: AppTheme.white,
      appBar: AppBar(
        title: Text(
          'STORE')
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            Text(
              'STORE!!!!!!!',
            )
          ],
        ),
      ),
    );
  }
}