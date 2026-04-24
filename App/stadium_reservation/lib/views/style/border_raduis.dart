import 'package:flutter/material.dart';

class AppBorderRadius{
  AppBorderRadius._();
  static const BorderRadius borderRadius1=BorderRadius.all(Radius.circular(10));
  static const BorderRadius borderRadius4=BorderRadius.all(Radius.circular(30));
  static const BorderRadius borderRadius2=BorderRadius.all(Radius.circular(5));
  static const BorderRadius borderRadius3=BorderRadius.only(bottomRight: Radius.circular(80),bottomLeft: Radius.circular(80));
  static const BorderRadius borderRadius5=BorderRadius.only(topRight: Radius.circular(80),topLeft: Radius.circular(80));
}