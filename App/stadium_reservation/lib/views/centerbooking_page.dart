import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:stadium_reservation/controllers/centerbooking_controller.dart';
import 'package:stadium_reservation/models/Dtos/centerbooking.dart';

class CenterbookingPage extends StatefulWidget {
  final String userId;
  final String centerId;

  CenterbookingPage({required this.userId, required this.centerId});

  @override
  _CenterbookingPage createState() => _CenterbookingPage();
}

class _CenterbookingPage extends State<CenterbookingPage> {
  final _formKey = GlobalKey<FormState>();
  final CenterbookingController bookingController = Get.put(CenterbookingController());

  DateTime? _beginTime;
  DateTime? _endTime;
  String _paymentMethod = 'OnArrived';
  String? _exchangeNumber;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Book Stadium')),
      body: Stack(
        children: [
          Positioned.fill(
            child: Image.asset(
              'assets/images/ground.png',
              fit: BoxFit.cover,
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(20.0),
            child: Form(
              key: _formKey,
              child: SingleChildScrollView(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: [
                    Text(
                      'Stadium Booking',
                      style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold, color: Colors.white),
                      textAlign: TextAlign.center,
                    ),
                    SizedBox(height: 20),
                    // Begin Time
                    TextFormField(
                      decoration: InputDecoration(labelText: 'Begin Time', filled: true, fillColor: Colors.white),
                      readOnly: true,
                      onTap: () async {
                        final selectedDate = await showDatePicker(
                          context: context,
                          initialDate: DateTime.now(),
                          firstDate: DateTime.now(),
                          lastDate: DateTime(2100),
                        );
                        if (selectedDate != null) {
                          final selectedTime = await showTimePicker(
                            context: context,
                            initialTime: TimeOfDay.now(),
                          );
                          if (selectedTime != null) {
                            setState(() {
                              _beginTime = DateTime(
                                selectedDate.year,
                                selectedDate.month,
                                selectedDate.day,
                                selectedTime.hour,
                                selectedTime.minute,
                              );
                            });
                          }
                        }
                      },
                      validator: (value) => _beginTime == null ? 'Please select a begin time' : null,
                      controller: TextEditingController(text: _beginTime?.toString() ?? ''),
                    ),
                    SizedBox(height: 10),
                    // End Time
                    TextFormField(
                      decoration: InputDecoration(labelText: 'End Time', filled: true, fillColor: Colors.white),
                      readOnly: true,
                      onTap: () async {
                        final selectedDate = await showDatePicker(
                          context: context,
                          initialDate: DateTime.now(),
                          firstDate: DateTime.now(),
                          lastDate: DateTime(2100),
                        );
                        if (selectedDate != null) {
                          final selectedTime = await showTimePicker(
                            context: context,
                            initialTime: TimeOfDay.now(),
                          );
                          if (selectedTime != null) {
                            setState(() {
                              _endTime = DateTime(
                                selectedDate.year,
                                selectedDate.month,
                                selectedDate.day,
                                selectedTime.hour,
                                selectedTime.minute,
                              );
                            });
                          }
                        }
                      },
                      validator: (value) => _endTime == null ? 'Please select an end time' : null,
                      controller: TextEditingController(text: _endTime?.toString() ?? ''),
                    ),
                    SizedBox(height: 10),
                    // Payment Method Dropdown
                    DropdownButtonFormField<String>(
                      decoration: InputDecoration(labelText: 'Payment Method', filled: true, fillColor: Colors.white),
                      value: _paymentMethod,
                      onChanged: (value) {
                        setState(() {
                          _paymentMethod = value!;
                          _exchangeNumber = null;
                        });
                      },
                      items: ['OnArrived', 'Alkurami', 'Alnamjm']
                          .map((method) => DropdownMenuItem(value: method, child: Text(method)))
                          .toList(),
                    ),
                    SizedBox(height: 10),
                    // Exchange Number Field
                    if (_paymentMethod == 'Alkurami' || _paymentMethod == 'Alnamjm')
                      TextFormField(
                        decoration: InputDecoration(labelText: 'Exchange Number', filled: true, fillColor: Colors.white),
                        onChanged: (value) => _exchangeNumber = value,
                        validator: (value) => _paymentMethod != 'OnArrived' && (value == null || value.isEmpty)
                            ? 'Exchange number is required'
                            : null,
                      ),
                    SizedBox(height: 20),
                    ElevatedButton(
                      onPressed: () async {
                        if (_formKey.currentState!.validate()) {
                          final booking = CenterBooking(
                            userId: widget.userId,
                            centerId: widget.centerId,
                            beginTime: _beginTime!,
                            endTime: _endTime!,
                            paymentMethod: _paymentMethod,
                            exchangeNumber: _exchangeNumber,
                          );
                          final success = await bookingController.addBooking(booking);
                          if (success) {
                            Get.snackbar('Success', 'Booking added successfully!');
                            Navigator.pop(context);
                          }
                        }
                      },
                      child: Text('Submit'),
                    ),
                  ],
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
