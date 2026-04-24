import 'package:device_preview/device_preview.dart';
import 'package:flutter/material.dart';
import 'package:get/get_navigation/src/root/get_material_app.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:stadium_reservation/views/auth/login.dart';
import 'package:stadium_reservation/views/home_page.dart';
import 'package:stadium_reservation/views/welcomepage.dart';

void main() {
  runApp(DevicePreview(
    enabled: true, // Set to `false` to disable it for release builds
    builder: (context) => const MyApp(), // Wrap your app with DevicePreview
  ));
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  Future<bool> _checkToken() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    // Add your token expiration check here, for now just check if it's not null
    return token != null;
  }

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return FutureBuilder<bool>(
      future: _checkToken(),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const MaterialApp(
            home: Scaffold(
              body: Center(child: CircularProgressIndicator()),
            ),
          );
        } else {
          return GetMaterialApp(
              debugShowCheckedModeBanner: false,
              title: 'OND',
              theme: ThemeData(
                fontFamily: "regular",
                appBarTheme: const AppBarTheme(
                  // backgroundColor: Colors.transparent,
                  elevation: 0,
                ),
              ),
              home: snapshot.data == true ? HomePage() : const Login());
          // home: MyCustomSplashScreen());
        }
      },
    );
  }
}
