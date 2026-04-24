import 'package:get/get.dart';
import 'package:stadium_reservation/models/pitchimageresponse.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

import 'package:stadium_reservation/shared/api_endpoints.dart';

class PitchImageController extends GetxController {
  var pitchImages = <PitchImageResponse>[].obs;
  var isLoading = true.obs;

  @override
  void onInit() {
    super.onInit();
    fetchPitchImages();
  }

  Future<void> fetchPitchImages() async {
    try {
      var url = Uri.parse(
          ApiEndPoints.baseUrl + ApiEndPoints.imageEndPoints.getStadium);
      var response = await http.get(url);

      if (response.statusCode == 200) {
        var jsonResponse = json.decode(response.body);
        if (jsonResponse['isSuccess']) {
          var data = jsonResponse['data'] as List;
          pitchImages.value =
              data.map((item) => PitchImageResponse.fromJson(item)).toList();
        }
      } else {
        Get.snackbar('Error', 'Failed to fetch data');
      }
    } catch (e) {
      Get.snackbar('Error', e.toString());
    } finally {
      isLoading.value = false;
    }
  }

  Future<void> getStadiumImagesByStadiumId(String stadiumId) async {
    try {
      var url = Uri.parse(ApiEndPoints.baseUrl +
          ApiEndPoints.imageEndPoints.getStadiumById
              .replaceFirst("{stadiumId}", stadiumId));

      var response = await http.get(url);

      if (response.statusCode == 200) {
        var jsonResponse = json.decode(response.body);
        if (jsonResponse['isSuccess']) {
          var data = jsonResponse['data'] as List;

          print('Data fetched: $data'); // Debug the data

          if (data.isNotEmpty) {
            try {
              pitchImages.value = data.map((item) {
                print(
                    '----------------------------------------------------- images');
                print('Mapping item: $item');
                print(
                    '----------------------------------------------------- images');
                // Debug individual items
                return PitchImageResponse.fromJson(item);
              }).toList();
            } catch (e) {
              print('Error during mapping: $e');
              Get.snackbar('Error', 'Failed to parse stadium images.');
            }
          } else {
            Get.snackbar('Warning', 'No images for this stadium');
            // print(data);

            // print('-----------------------------------------------------');
          }
        }
      } else {
        Get.snackbar('Error', 'Failed to fetch data');
      }
    } catch (e) {
      Get.snackbar('Error', e.toString());
    } finally {
      isLoading.value = false;
    }
  }
}
