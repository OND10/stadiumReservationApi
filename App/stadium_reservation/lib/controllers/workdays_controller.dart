import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:stadium_reservation/models/Dtos/workdayrequestdto.dart';
import 'package:stadium_reservation/models/workdaysresponse.dart';
import 'package:stadium_reservation/shared/api_endpoints.dart';

import 'package:http/http.dart' as http;
import 'package:stadium_reservation/views/home_page.dart';
import 'package:stadium_reservation/views/style/color_app.dart';

class WorkdaysController extends GetxController {
  var works = <Workdaysresponse>[].obs;
  var stadiumCenters = [].obs;
  var isLoading = false.obs;

  final dayOfWeekController = TextEditingController();
  final beginWorkTimeController = TextEditingController();
  final endWorkTimeController = TextEditingController();
  var selectedStadiumCenterId = ''.obs;

  @override
  void onInit() {
    super.onInit();
    fetchStadiumCenters(); // Fetch Stadium Centers when the page loads
  }

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

  Future<void> GetCenterWorkDays(String centerId) async {
    isLoading.value = false;

    var url = Uri.parse(ApiEndPoints.baseUrl +
        ApiEndPoints.workDaysEndPoints.getwordDaysCenter
            .replaceFirst("{centerId}", centerId));
    try {
      final response = await http.get(url);

      print(
          "----------------------------------------------------------------------------");
      print(response.body);
      print(
          "----------------------------------------------------------------------------");

      if (response.statusCode == 200) {
        var data = json.decode(response.body) as List;
        print(data);
        works.value = data
            .map((workJson) => Workdaysresponse.fromJson(workJson))
            .toList();
      } else {
        Get.snackbar('Error', 'Failed to load WorkDays');
      }
    } catch (e) {
      Get.snackbar('Error', 'An error occurred while fetching data');
    } finally {
      isLoading.value = false; // Stop loading
    }
  }

  Future<void> AddworkDayCenter() async {
    if (selectedStadiumCenterId.value.isEmpty) {
      Get.snackbar('Error', 'Please select a stadium center');
      return;
    }

    Workdayrequestdto newWorkDay = Workdayrequestdto(
      beginWorkTime: beginWorkTimeController.text,
      dayOfWeek: int.parse(dayOfWeekController.text),
      endWorkTime: endWorkTimeController.text,
      stadiumCenterId: selectedStadiumCenterId
          .value, // This should now hold the correct value
    );

    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? token = prefs.getString('token');

    //check if token is that came from the session is null
    if (token == null) {
      Get.snackbar('Error', 'You are not authorized to add workDay',
          snackPosition: SnackPosition.BOTTOM,
          backgroundColor: const Color.fromARGB(255, 12, 46, 30),
          colorText: Colors.red);
      return;
    }

    final response = await http.post(
      Uri.parse('https://localhost:7049/api/WorkDay'),
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer $token',
      },
      body: json.encode(newWorkDay.toJson()),
    );

    if (response.statusCode == 200) {
      Get.snackbar('Success', 'Center Workday added successfully',
          snackPosition: SnackPosition.BOTTOM,
          backgroundColor: AppColors.mainColor,
          colorText: AppColors.whiteColor);

      //clear form cache
      beginWorkTimeController.clear();
      endWorkTimeController.clear();
      dayOfWeekController.clear();

      Get.to(() => HomePage());
    } else {
      Get.snackbar('Error', 'Failed to add workDay',
          snackPosition: SnackPosition.BOTTOM,
          backgroundColor: const Color.fromARGB(255, 12, 46, 30),
          colorText: Colors.red);
    }
  }
}
