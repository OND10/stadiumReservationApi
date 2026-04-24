import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/login_controller.dart';
import 'package:stadium_reservation/controllers/stadium_controller.dart';
import 'package:stadium_reservation/views/stadiumdetails.dart';
import 'package:stadium_reservation/views/stadiumform.dart';

class StadiumImagePage extends StatelessWidget {
  final String centerId; // Center ID passed from ProductPage
  final StadiumController stadiumController = Get.put(StadiumController());
  final LoginController loginController = Get.put(LoginController());

  StadiumImagePage({Key? key, required this.centerId}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    // Fetch stadiums by center ID
    stadiumController.getStadiumByCenterId(centerId);

    return Scaffold(
      appBar: AppBar(
        title: Text('Grounds'),
        backgroundColor: Colors.transparent,
        elevation: 0,
      ),
      body: Obx(() {
        if (stadiumController.isLoading.value) {
          return Center(
            child: CircularProgressIndicator(
              color: Colors.white, // Optional, based on your design
            ),
          );
        } else if (stadiumController.stadiums.isEmpty) {
          return Center(
            child: Text(
              'No stadiums found for this center.',
              style: TextStyle(color: Colors.white),
            ),
          );
        } else {
          return Stack(
            children: [
              // Background Image
              Positioned.fill(
                child: Image.asset(
                  'assets/images/ground.png',
                  fit: BoxFit.cover,
                ),
              ),
              // Overlay with content
              ListView.builder(
                padding: const EdgeInsets.all(16.0),
                itemCount: stadiumController.stadiums.length,
                itemBuilder: (context, index) {
                  final stadium = stadiumController.stadiums[index];
                  return Card(
                    color: Color.fromARGB(255, 66, 66, 66).withOpacity(
                        0.2), // Increased opacity for better visibility
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(15),
                    ),
                    margin: const EdgeInsets.symmetric(vertical: 10),
                    child: ListTile(
                      onTap: () {
                        Get.to(() => StadiumDetails(stadiumId: stadium.id!));
                      },
                      leading: CircleAvatar(
                        backgroundImage: AssetImage(
                            'assets/images/appm.png'), // Placeholder image
                        radius: 30,
                      ),
                      title: Text(
                        stadium.name ??
                            'Unknown Stadium', // Safely accessing name
                        style: TextStyle(color: Colors.white),
                      ),
                      subtitle: Row(
                        children: [
                          Icon(Icons.star, color: Colors.yellow, size: 20),
                          Icon(Icons.star, color: Colors.yellow, size: 20),
                          Icon(Icons.star, color: Colors.yellow, size: 20),
                          Icon(Icons.star, color: Colors.yellow, size: 20),
                          Icon(Icons.star, color: Colors.yellow, size: 20),
                        ],
                      ),
                      trailing: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Text(
                            stadium.noOfPlayers ?? 'N/A',
                            style: TextStyle(color: Colors.white),
                          ),
                          SizedBox(height: 5),
                          Text(
                            stadium.priceInHour != null
                                ? '${stadium.priceInHour} YE'
                                : 'Price Unavailable', // Safely accessing price
                            style: TextStyle(color: Colors.white),
                          ),
                        ],
                      ),
                    ),
                  );
                },
              ),
            ],
          );
        }
      }),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          // Get.to(() => StadiumFormPage());
          Navigator.push(
            context,
            MaterialPageRoute(builder: (context) => StadiumFormPage()),
          );
        },
        child: Icon(
          Icons.add,
          color: Colors.white,
        ),
        backgroundColor:
            Color.fromARGB(255, 53, 90, 87).withOpacity(0.2), // Better opacity
      ),
    );
  }
}
