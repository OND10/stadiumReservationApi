import 'package:flutter/material.dart';
import 'package:stadium_reservation/views/style/color_app.dart';


Widget TextTitle2({
  required String text
})=>Text(
  text,
  style: const TextStyle(color: AppColors.whiteColor, fontSize: 20,fontWeight: FontWeight.bold),
);
Widget Textsub({required String text}) => Text(
      text,
      style: const TextStyle(
          color: Color.fromARGB(255, 248, 248, 248),
          fontSize: 15,
          fontWeight: FontWeight.bold),
    );
