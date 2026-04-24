// import 'dart:convert';
// import 'dart:io';
// import 'package:flutter/material.dart';
// import 'package:get/get.dart';
// import 'package:http/http.dart' as http;
// import 'package:stadium_reservation/shared/api_endpoints.dart';
// import 'package:stadium_reservation/views/auth/login.dart';
// import 'package:http_parser/http_parser.dart';

// class RegisterController extends GetxController {
//   TextEditingController emailcontroller = TextEditingController();
//   TextEditingController userNamecontroller = TextEditingController();
//   TextEditingController passwordcontroller = TextEditingController();
//   TextEditingController phoneNumbercontroller = TextEditingController();

//   Future<void> regitserUserWithImage(File? imageFile) async {
//     if (imageFile == null) {
//       print("================================================");
//       print("Image is null");

//       print("================================================");
//     }
//     try {
//       var url = Uri.parse('http://localhost:36320/api/Auth/register');

//       var request = http.MultipartRequest('POST', url);
//       request.fields['email'] = emailcontroller.text;
//       request.fields['username'] = userNamecontroller.text;
//       request.fields['password'] = passwordcontroller.text;
//       request.fields['phone_number'] = phoneNumbercontroller.text;

//       if (imageFile != null) {
//         var fileStream = http.ByteStream(imageFile.openRead());
//         var length = await imageFile.length();
//         var multipartFile = http.MultipartFile(
//           'file',
//           fileStream,
//           length,
//           filename: imageFile.path.split('/').last,
//           contentType: MediaType('image', 'jpeg'),
//         );
//         request.files.add(multipartFile);
//       }

//       // Send the request and wait for response
//       var response = await request.send();

//       // Read the response body
//       var responseBody = await response.stream.bytesToString();
//       var responseData = jsonDecode(responseBody);
//       print("================================================");
//       print(responseBody);
//       print(responseData);
//       if (response.statusCode == 200) {
//         // Navigate to login if registration is successful
//         Get.to(() => Login());
//       } else {
//         // Show the response message from the backend
//         String errorMessage = responseData['message'] ?? 'Registration failed';
//         Get.snackbar("Error", errorMessage,
//             snackPosition: SnackPosition.BOTTOM, backgroundColor: Colors.red);
//       }
//     } catch (e) {
//       Get.snackbar("Error", e.toString(),
//           snackPosition: SnackPosition.BOTTOM, backgroundColor: Colors.red);
//       print(e.toString());
//     }
//   }
// }
/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
///Register without image
/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////

import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;
import 'package:stadium_reservation/shared/api_endpoints.dart';
import 'package:stadium_reservation/views/auth/login.dart';

class RegisterController extends GetxController {
  TextEditingController emailcontroller = TextEditingController();
  TextEditingController userNamecontroller = TextEditingController();
  TextEditingController passwordcontroller = TextEditingController();
  TextEditingController phoneNumbercontroller = TextEditingController();

  final Future<SharedPreferences> _prefs = SharedPreferences.getInstance();

  Future<void> regitserUser() async {
    try {
      var headers = {'Content-Type': 'application/json'};
      var url = Uri.parse(
          ApiEndPoints.baseUrl + ApiEndPoints.authEndPoints.registerUser);
      Map body = {
        'email': emailcontroller.text,
        'userName': userNamecontroller.text,
        'password': passwordcontroller.text,
        'phoneNumber': phoneNumbercontroller.text
      };

      http.Response response =
          await http.post(url, body: jsonEncode(body), headers: headers);

      if (response.statusCode == 200) {
        final json = jsonDecode(response.body);
        if (json['isSuccess'] == true) {
          var user_email = json['data']['email'];
          print(user_email);
          final SharedPreferences? prefs = await _prefs;

          await prefs?.setString('email', user_email);
          userNamecontroller.clear();
          passwordcontroller.clear();
          phoneNumbercontroller.clear();
          Get.off(const Login());
        } else {
          throw jsonDecode(response.body)["message"] ?? "Unknows Error Occured";
        }
      } else {
        throw jsonDecode(response.body)["message"] ?? "Unknows Error Occured";
      }
    } catch (e) {
      Get.back();
      showDialog(
          context: Get.context!,
          builder: (context) {
            return SimpleDialog(
              title: Text('Error'),
              contentPadding: EdgeInsets.all(20),
              children: [Text(e.toString())],
            );
          });
    }
  }
}
