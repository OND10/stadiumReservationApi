class PitchImageResponse {
  String fileName;
  String imageUrl;
  DateTime createdOn;
  String stadiumId;

  PitchImageResponse({
    required this.fileName,
    required this.imageUrl,
    required this.createdOn,
    required this.stadiumId,
  });

  factory PitchImageResponse.fromJson(Map<String, dynamic> json) {
  try {
    return PitchImageResponse(
      fileName: json['fileName'] ?? '',  // Provide default value if null
      imageUrl: json['imageUrl'] ?? '',  // Provide default value if null
      createdOn: DateTime.parse(json['createdOn']),  // This could throw an exception
      stadiumId: json['stadiumId'] ?? '',
    );

    
  } catch (e) {
    print('Error parsing PitchImageResponse: $e');
    throw Exception('Failed to parse PitchImageResponse');
  }
}

}
