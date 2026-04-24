import 'package:flutter/material.dart';
import 'package:stadium_reservation/views/components/app_icons.dart';
import 'package:stadium_reservation/views/components/snackBarFun.dart';
import 'package:stadium_reservation/views/components/textApp.dart';
import 'package:stadium_reservation/views/style/border_raduis.dart';
import 'package:stadium_reservation/views/style/color_app.dart';

Widget StaduimCard({
  required Object id,
  required String name,
  required String phoneNumber,
  required String location,
  required bool dressingRoomVisible,
  required String imageUrl,
  required String owned_By,
  required BuildContext context,
}) =>
    Container(
      width: 200,
      height: 250,
      margin: const EdgeInsets.only(left: 20),
      decoration: const BoxDecoration(
        color: Color(0xFF051414),
        borderRadius: AppBorderRadius.borderRadius1,
      ),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        mainAxisSize: MainAxisSize.max,
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          Expanded(
            child: GestureDetector(
              onTap: () {
              },
              child: Hero(
                tag: id,
                child: Container(
                  margin: const EdgeInsets.all(8),
                  padding: const EdgeInsets.all(10),
                  decoration: BoxDecoration(
                    borderRadius: AppBorderRadius.borderRadius2,
                    color: AppColors.lightBlackColor3,
                    image: DecorationImage(
                      image: NetworkImage(imageUrl),
                      fit: BoxFit.cover,
                    ),
                  ),
                ),
              ),
            ),
          ),
          const SizedBox(height: 2),
          Textsub(text: name),
          const SizedBox(height: 2),
          SingleChildScrollView(
            scrollDirection: Axis.horizontal,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                Icon(
                  Icons.person_outline,
                  color: AppColors.whiteColor,
                ),
                Textsub(text: owned_By)
              ],
            ),
          ),
          SingleChildScrollView(
            scrollDirection: Axis.horizontal,
            child: Row(
              children: [
                Padding(
                  padding: const EdgeInsets.all(4),
                  child: GestureDetector(
                    onTap: () {
                      
                    },
                    child: ICon(
                      iconic: Icons.location_city,
                      colorIcon: const Color.fromARGB(255, 255, 255, 255),
                    ),
                  ),
                ),
                Textsub(text: location),
              ],
            ),
          ),
        ],
      ),
    );
