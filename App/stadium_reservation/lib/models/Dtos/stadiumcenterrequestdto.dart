class Stadiumcenterrequestdto {
  String name;
  String phoneNumber;
  String location;
  String owned_By;
  bool dressingRoomVisible;

  Stadiumcenterrequestdto({
    required this.name,
    required this.phoneNumber,
    required this.location,
    required this.owned_By,
    required this.dressingRoomVisible,
  });

  // Convert the DTO to a JSON object to send in the POST request
  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'phoneNumber': phoneNumber,
      'location': location,
      'owned_By': owned_By,
      'dressingRoomVisible': dressingRoomVisible,
    };
  }
}
