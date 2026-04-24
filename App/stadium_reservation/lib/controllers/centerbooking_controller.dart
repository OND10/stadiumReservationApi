import 'package:get/get.dart';
import 'package:stadium_reservation/models/Dtos/centerbooking.dart';
import 'package:stadium_reservation/shared/api_endpoints.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class CenterbookingController extends GetxController {
  Future<bool> addBooking(CenterBooking booking) async {
    final url = Uri.parse(ApiEndPoints.baseUrl +
        ApiEndPoints.centerBookingEndPoints.CenterBooking);

    try {
      final response = await http.post(
        url,
        headers: {'Content-Type': 'application/json'},
        body: json.encode(booking.toJson()),
      );
      if (response.statusCode == 200 || response.statusCode == 201) {
        return true;
      } else {
        throw Exception('Failed to add booking');
      }
    } catch (e) {
      Get.snackbar('Error', 'Failed to add booking: $e');
      return false;
    }
  }
}
