import 'package:flutter/material.dart';
import 'package:recycle_market/layout/blog/blog.dart';
import 'package:recycle_market/layout/custom_drawer/drawer_user_controller.dart';
import 'package:recycle_market/layout/custom_drawer/home_drawer.dart';
import 'package:recycle_market/layout/menu/app_theme.dart';
import 'package:recycle_market/layout/menu/home_screen.dart';
import 'package:recycle_market/layout/store/store.dart';

class NavigationHomeScreen extends StatefulWidget {
  @override
  _NavigationHomeScreenState createState() => _NavigationHomeScreenState();
}

class _NavigationHomeScreenState extends State<NavigationHomeScreen> {
  Widget screenView;
  DrawerIndex drawerIndex;
  AnimationController sliderAnimationController;

  @override
  void initState() {
    drawerIndex = DrawerIndex.HOME;
    screenView = const MyHomePage();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      color: AppTheme.nearlyWhite,
      child: SafeArea(
        top: false,
        bottom: false,
        child: Scaffold(
          backgroundColor: AppTheme.nearlyWhite,
          body: DrawerUserController(
            screenIndex: drawerIndex,
            drawerWidth: MediaQuery.of(context).size.width * 0.75,
            animationController: (AnimationController animationController) {
              sliderAnimationController = animationController;
            },
            onDrawerCall: (DrawerIndex drawerIndexdata) {
              changeIndex(drawerIndexdata);
            },
            screenView: screenView,
          ),
        ),
      ),
    );
  }

  void changeIndex(DrawerIndex drawerIndexdata) {
    if (drawerIndex != drawerIndexdata) {
      drawerIndex = drawerIndexdata;
      if (drawerIndex == DrawerIndex.HOME) {
        setState(() {
          screenView = const MyHomePage();
        });
      } else if (drawerIndex == DrawerIndex.Store) {
        setState(() {
          screenView = const StorePage();
        });
      } else if (drawerIndex == DrawerIndex.Blog) {
        setState(() {
          screenView = const BlogPage();
        });
      }
    }
  }
}
