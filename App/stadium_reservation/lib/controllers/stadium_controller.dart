// lib/controllers/stadium_controller.dart
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:http/http.dart' as http;
import 'package:stadium_reservation/models/Dtos/stadiumrequestdto.dart';
import 'package:stadium_reservation/models/stadiumresponse.dart';
import 'package:stadium_reservation/shared/api_endpoints.dart';
import 'package:stadium_reservation/views/home_page.dart';

class StadiumController extends GetxController {
  var stadiums = <Stadiumresponse>[].obs; // Observable list of stadiums
  var isLoading = false.obs;
  var stadiumCenters = [].obs;
  var stadium = Stadiumresponse().obs;

  // Form controllers
  final nameController = TextEditingController();
  final priceInHourController = TextEditingController();
  final typeController = TextEditingController();
  final noOfPlayersController = TextEditingController();
  var selectedStadiumCenterId = ''.obs;

  @override
  void onInit() {
    super.onInit();
    fetchStadiumCenters(); // Fetch Stadium Centers when the page loads
  }

  // Fetch list of stadium centers from the API
  Future<void> fetchStadiumCenters() async {
    final response = await http
        .get(Uri.parse('https://localhost:7049/api/StadiumCenter/getCenter'));

    if (response.statusCode == 200) {
      var jsonData = json.decode(response.body);
      var centersData = jsonData['data'];
      stadiumCenters.value = centersData;
    } else {
      Get.snackbar('Error', 'Failed to load stadium centers');
    }
  }

  Future<void> getStadiumByCenterId(String centerId) async {
    isLoading.value = true; // Start loading
    var url = Uri.parse(ApiEndPoints.baseUrl +
        ApiEndPoints.stadiumEndPoints.getStadiumByCenter
            .replaceFirst("{centerId}", centerId));
    try {
      final response = await http.get(url);

      if (response.statusCode == 200) {
        var data = json.decode(response.body)['data'] as List;
        stadiums.value = data
            .map((stadiumJson) => Stadiumresponse.fromJson(stadiumJson))
            .toList();
      } else {
        Get.snackbar('Error', 'Failed to load stadiums');
      }
    } catch (e) {
      Get.snackbar('Error', 'An error occurred while fetching data');
    } finally {
      isLoading.value = false; // Stop loading
    }
  }

  // Submit form data to the API
  Future<void> addStadium() async {
    StadiumRequestDto newStadium = StadiumRequestDto(
      name: nameController.text,
      priceInHour: int.parse(priceInHourController.text),
      type: typeController.text,
      noOfPlayers: noOfPlayersController.text,
      stadiumCenterId: selectedStadiumCenterId.value,
    );

    final response = await http.post(
      Uri.parse('https://localhost:7049/api/Stadium'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(newStadium.toJson()),
    );

    if (response.statusCode == 200) {
      Get.snackbar('Success', 'Stadium added successfully',
          snackPosition: SnackPosition.BOTTOM,
          backgroundColor: const Color.fromARGB(255, 12, 46, 30),
          colorText: Colors.white);
      Get.to(()=>HomePage());
    } else {
      Get.snackbar('Error', 'Failed to add stadium',
          snackPosition: SnackPosition.BOTTOM,
          backgroundColor: const Color.fromARGB(255, 12, 46, 30),
          colorText: Colors.red);
    }
  }

  Future<void> getStadiumById(String stadiumId) async {
    isLoading.value = false; // Start loading
    var url = Uri.parse(ApiEndPoints.baseUrl +
        ApiEndPoints.stadiumEndPoints.getStadiumById
            .replaceFirst("{stadiumId}", stadiumId));

    // print("Fetching Stadium ID: $stadiumId"); // Add debug print
    // print("Request URL: $url"); // Print the request URL

    try {
      final response = await http.get(url);
      // print("Response Status Code: ${response.statusCode}");
      // print(
      //     "Response Body: ${response.body}"); // Print response body for debugging

      if (response.statusCode == 200) {
        var data = json.decode(response.body)['data'];
        print("Parsed Data: $data"); // Check the data being parsed
        stadium.value = Stadiumresponse.fromJson(data);
      } else {
        // Get.snackbar('Error', 'Failed to load stadium');
      }
    } catch (e) {
      print("Error occurred: $e"); // Log the error for better debugging
      Get.snackbar('Error', 'An error occurred while fetching data');
    } finally {
      isLoading.value = false; // Stop loading
    }
  }
}
