import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/pitchimage_controller.dart';
import 'package:stadium_reservation/controllers/stadium_controller.dart';
import 'package:stadium_reservation/controllers/stadiumcenter_controller.dart';
import 'package:stadium_reservation/controllers/workdays_controller.dart';
import 'package:stadium_reservation/views/centerbooking_page.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:shared_preferences/shared_preferences.dart';

class StadiumDetails extends StatefulWidget {
  final String stadiumId;

  StadiumDetails({Key? key, required this.stadiumId}) : super(key: key);

  @override
  _StadiumDetailsState createState() => _StadiumDetailsState();
}

class _StadiumDetailsState extends State<StadiumDetails> {
  final StadiumController stadiumController = Get.put(StadiumController());
  final StadiumcenterController stadiumCenterController =
      Get.put(StadiumcenterController());
  final WorkdaysController workdaysController = Get.put(WorkdaysController());
  final PitchImageController pitchController = Get.put(PitchImageController());
  String? userId;

  @override
  void initState() {
    super.initState();
    // Fetch data only once when the widget is first built
    fetchInitialData();
    _loadUserId();
  }

  Future<void> _loadUserId() async {
    final prefs = await SharedPreferences.getInstance();
    setState(() {
      userId = prefs.getString('userId'); // Retrieve the user ID
    });
  }

  Future<void> _openWhatsApp(String phoneNumber) async {
    var whatsappUrl = "https://wa.me/$phoneNumber";
    if (await canLaunch(whatsappUrl)) {
      await launch(whatsappUrl);
    } else {
      Get.snackbar("Error", "Could not open WhatsApp");
    }
  }

  void fetchInitialData() async {
    // Fetch stadium details first
    await stadiumController.getStadiumById(widget.stadiumId);

    // Fetch stadium images
    await pitchController.getStadiumImagesByStadiumId(widget.stadiumId);

    // Get the center ID from the fetched stadium data and then fetch the workdays and center data
    final stadiumCenterId =
        stadiumController.stadium.value.stadiumCenterId ?? "any";

    // Fetch stadium center and workdays data
    await stadiumCenterController.getStadiumCenterById(stadiumCenterId);
    await workdaysController.GetCenterWorkDays(stadiumCenterId);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
      // drawer: AppDrawer(),
      body: Stack(
        children: [
          // Background Image
          Positioned.fill(
            child: Image.asset(
              'assets/images/ground.png',
              fit: BoxFit.cover,
              // Ensures the image covers the entire screen
              width: MediaQuery.of(context).size.width,
              height: MediaQuery.of(context).size.height,
            ),
          ),
          // Main content with margin at the bottom
          Obx(() {
            // Check loading states for all controllers
            if (stadiumController.isLoading.value ||
                stadiumCenterController.isLoding.value ||
                workdaysController.isLoading.value) {
              return Center(
                child: CircularProgressIndicator(),
              );
            } else if (stadiumController.stadium.value == null) {
              return Center(
                child: Text(
                  'Failed to load stadium details',
                  style: TextStyle(color: Colors.red),
                ),
              );
            } else {
              return Center(
                child: Padding(
                  padding: const EdgeInsets.only(
                      bottom: 20.0), // Add margin at the bottom
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        'Stadium Details',
                        style: TextStyle(
                          fontSize: 28,
                          fontWeight: FontWeight.bold,
                          color: Colors.white,
                        ),
                      ),
                      SizedBox(
                        height: 25,
                      ),
                      CarouselSlider(
                        options: CarouselOptions(
                          height: 200.0,
                          autoPlay: false,
                        ),
                        items: pitchController.pitchImages.map((image) {
                          return Builder(
                            builder: (BuildContext context) {
                              return ClipRRect(
                                borderRadius: BorderRadius.circular(
                                    20), // Set the border radius to 20
                                child: Image.network(
                                  image.imageUrl,
                                  fit: BoxFit
                                      .cover, // Ensure the image covers the entire area
                                ),
                              );
                            },
                          );
                        }).toList(),
                      ),

                      SizedBox(height: 20),
                      // Stadium Information
                      GroundInfo(
                          text:
                              'Name: ${stadiumController.stadium.value.name ?? 'N/A'}'),
                      GroundInfo(
                          text:
                              'Price/Hour: ${stadiumController.stadium.value.priceInHour ?? 'N/A'}'),
                      GroundInfo(
                          text:
                              'No. of Players: ${stadiumController.stadium.value.noOfPlayers ?? 'N/A'}'),
                      SizedBox(height: 10),
                      GroundInfo(
                          text:
                              'Center Phone: ${stadiumCenterController.stadiumcenter.value.phoneNumber ?? 'N/A'}'),
                      // Workdays Information
                      GroundInfo(
                          text:
                              'Stadium Type: ${stadiumController.stadium.value.type ?? 'N/A'}'),
                      Text(
                        'Working Days:',
                        style: TextStyle(
                          fontSize: 20,
                          color: Colors.white,
                        ),
                      ),
                      Column(
                        children: workdaysController.works.map((workday) {
                          return GroundInfo(
                            text:
                                '${workday.dayOfWeek}: ${workday.beginWorkTime} - ${workday.endWorkTime}',
                          );
                        }).toList(),
                      ),
                      SizedBox(height: 20),
                      ElevatedButton(
                        style: ElevatedButton.styleFrom(
                          backgroundColor: Colors.green, // Button color
                          padding: EdgeInsets.symmetric(
                              horizontal: 40, vertical: 15),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10),
                          ),
                        ),
                        onPressed: () {
                          // Handle booking logic here
                          // _openWhatsApp(
                          // '${stadiumCenterController.stadiumcenter.value.phoneNumber ?? 'N/A'}');
                          final centerId =
                              stadiumController.stadium.value.stadiumCenterId ??
                                  "any";
                          if (userId != null) {
                            Get.to(() => CenterbookingPage(
                                  userId: userId!,
                                  centerId: centerId,
                                ));
                          } else {
                            Get.snackbar("Error", "User ID not available");
                          }
                        },
                        child: Text(
                          'Book Now',
                          style: TextStyle(fontSize: 18, color: Colors.white),
                        ),
                      ),
                    ],
                  ),
                ),
              );
            }
          }),
        ],
      ),
    );
  }
}

class GroundInfo extends StatelessWidget {
  final String text;
  GroundInfo({required this.text});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 5.0),
      child: Text(
        text,
        style: TextStyle(
          fontSize: 18,
          color: Colors.white,
        ),
      ),
    );
  }
}
