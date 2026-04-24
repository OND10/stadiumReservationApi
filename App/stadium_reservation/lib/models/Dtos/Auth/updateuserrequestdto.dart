class Updateuserrequestdto {
  String name;
  String phoneNumber;

  Updateuserrequestdto({required this.name, required this.phoneNumber});

  Map<String, dynamic> toJson() {
    return {'name': name, 'phoneNumber': phoneNumber};
  }
}
