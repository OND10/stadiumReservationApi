import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/models/Dtos/stadiumcenterrequestdto.dart';
import 'package:stadium_reservation/models/stadiumcenterresponse.dart';
import 'package:http/http.dart' as http;
import 'package:stadium_reservation/shared/api_endpoints.dart';

class StadiumcenterController extends GetxController {
  var stadiumcenters = <Stadiumcenterresponse>[].obs;
  var stadiumcenter = Stadiumcenterresponse().obs;
  var isLoding = true.obs;

  //controllers for performing post
  final nameController = TextEditingController();
  final phoneNumberController = TextEditingController();
  final locationController = TextEditingController();
  final ownedByController = TextEditingController();
  final dressingRoomVisibleController = TextEditingController();

  @override
  void onInit() {
    super.onInit();
    fetchStadiumCenter();
  }

  Future<void> fetchStadiumCenter() async {
    try {
      var url = Uri.parse(ApiEndPoints.baseUrl +
          ApiEndPoints.stadiumCenterEndPoints.getAllStadiumCenter);
      var response = await http.get(url);

      if (response.statusCode == 200) {
        var jsonResponse = json.decode(response.body);
        if (jsonResponse['isSuccess']) {
          var data = jsonResponse['data'] as List;
          stadiumcenters.value =
              data.map((item) => Stadiumcenterresponse.fromJson(item)).toList();
        }
      } else {
        Get.snackbar('Error', 'Failed to fetch data');
      }
    } catch (e) {
      Get.snackbar('Error', e.toString());
    } finally {
      isLoding.value = false;
    }
  }

  Future<void> getStadiumCenterById(String id) async {
    isLoding.value = false; // Start loading
    var url = Uri.parse(ApiEndPoints.baseUrl +
        ApiEndPoints.stadiumCenterEndPoints.getStadiumCenterById
            .replaceFirst("{id}", id));
    try {
      final response = await http.get(url);

      if (response.statusCode == 200) {
        var data =
            json.decode(response.body)['data']; // Expecting a single user
        stadiumcenter.value =
            Stadiumcenterresponse.fromJson(data); // Assign single user
      } else {
        Get.snackbar('Error', 'Failed to load center ');
      }
    } catch (e) {
      Get.snackbar('Error', 'An error occurred while fetching center data');
    } finally {
      isLoding.value = false;
    }
  }

  // Future<void> addStadiumCenter() async {
  //   Stadiumcenterrequestdto newStadium = Stadiumcenterrequestdto(
  //     name: nameController.text,
  //     phoneNumber: phoneNumberController.text,
  //     location: locationController.text,
  //     owned_By: ownedByController.text,
  //     dressingRoomVisible:
  //         bool.parse(dressingRoomVisibleController.value as String),
  //   );

  //   try{
  //     var url = Uri.parse('https://localhost:7049/api/StadiumCenter');
  //     var request = http.MultipartRequest('POST', url);

  //   }

  //   final response = await http.post(
  //     Uri.parse('https://localhost:7049/api/StadiumCenter'),
  //     headers: {'Content-Type': 'application/json'},
  //     body: json.encode(newStadium.toJson()),
  //   );

  //   if (response.statusCode == 200) {
  //     Get.snackbar('Success', 'Stadium added successfully',
  //         snackPosition: SnackPosition.BOTTOM,
  //         backgroundColor: const Color.fromARGB(255, 12, 46, 30),
  //         colorText: Colors.white);
  //     Get.to(HomePage());
  //   } else {
  //     Get.snackbar('Error', 'Failed to add stadium',
  //         snackPosition: SnackPosition.BOTTOM,
  //         backgroundColor: const Color.fromARGB(255, 12, 46, 30),
  //         colorText: Colors.red);
  //   }
  // }
}
