import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;
import 'package:stadium_reservation/shared/api_endpoints.dart';
import 'package:stadium_reservation/views/auth/login.dart';
import 'package:stadium_reservation/views/home_page.dart';

class LoginController extends GetxController {
  TextEditingController userNamecontroller = TextEditingController();
  TextEditingController passwordcontroller = TextEditingController();

  final Future<SharedPreferences> _prefs = SharedPreferences.getInstance();

  Future<void> loginUser() async {
    try {
      var headers = {'Content-Type': 'application/json'};
      var url = Uri.parse(
          ApiEndPoints.baseUrl + ApiEndPoints.authEndPoints.loginUser);
      Map body = {
        'userName': userNamecontroller.text,
        'password': passwordcontroller.text,
      };

      http.Response response =
          await http.post(url, body: jsonEncode(body), headers: headers);

      if (response.statusCode == 200) {
        final json = jsonDecode(response.body);
        if (json['isSuccess'] == true) {
          var token = json['data']['token'];
          var roles = json['data']['roles'];
          var userId = json['data']['user']['id'];
          var userName = json['data']['user']['name'];
          // var userImage = json['data']['user']['imageUrl']; // This is likely a List
          final SharedPreferences? prefs = await _prefs;

          print('-----------------------------------');
          // print(user);
          print('-----------------------------------');
          await prefs?.setString('token', token);
          await prefs?.setString('userId', userId);
          await prefs?.setString('userName', userName);
          // await prefs?.setString('userImage', userImage);
          await prefs?.setString('roles', jsonEncode(roles));

          // Convert the List of roles to a JSON string and save it
          String? rolesString = prefs?.getString('roles');
          if (rolesString != null) {
            List<dynamic> roles = jsonDecode(rolesString);
            // print('Roles: $roles'); // You can use roles as a list now
          }

          userNamecontroller.clear();
          passwordcontroller.clear();

          // Navigate to ProductPage
          Get.to(() => HomePage());
        } else if (json['isSuccess'] == false) {
          throw jsonDecode(response.body)["message"] ??
              "Unknown Error Occurred";
        }
      } else {
        throw jsonDecode(response.body)["message"] ?? "Unknown Error Occurred";
      }
    } catch (e) {
      showDialog(
        context: Get.context!,
        builder: (context) {
          return SimpleDialog(
            title: const Text('Error'),
            contentPadding: EdgeInsets.all(20),
            children: [Text(e.toString())],
          );
        },
      );
    }
  }

  Future<void> logout() async {
    final SharedPreferences prefs = await _prefs;
    await prefs.clear();
    Get.offAll(const Login());
  }
}
