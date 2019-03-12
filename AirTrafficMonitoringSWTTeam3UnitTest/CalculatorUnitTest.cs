﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AirTrafficMonitoringSWTTeam3;
using TransponderReceiver;
using NSubstitute;

namespace AirTrafficMonitoringSWTTeam3UnitTest
{
    [TestFixture]
    public class CalculatorUnitTest
    {

        private ITransponderReceiver _fakeTransponderReceiver;
        private Calculator uut;

        [SetUp]
        public void SetUp()
        {
            _fakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            uut = new Calculator(_fakeTransponderReceiver);
        }

      
       
        [Test]
        public void AirSpaceOnlyFlightsWithinTheSpace()
        {
            List<string> testData = new List<string>();
            testData.Add("ATR423;85045;12932;14000;20151006213456789");
            testData.Add("BCD123;10005;85001;12000;20151006213456789");
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");
            testData.Add("XYZ987;90059;90654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.currentAircrafts.Count.Equals(1));
        }

        [Test]
        public void AirspaceCorrectXCoordinate()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.currentAircrafts[0].XCoordinate.Equals(85000));
        }

        [Test]
        public void AirspaceCorrectYCoordinate()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.currentAircrafts[0].YCoordinate.Equals(75654));
        }
        [Test]
        public void AirspaceCorrectAltitude()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.currentAircrafts[0].Altitude.Equals(4000));
        }

        [Test]
        public void AirspaceCorrectTimestamp()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.currentAircrafts[0].Timestamp.Equals(new DateTime(2015,10,06,21,34,56,789)));
        }

        [Test]
        public void AirspaceCorrectTag()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.currentAircrafts[0].Tag.Equals("XYZ987"));
        }




        [Test]
        public void CalculateCompassCourse_()
        {
            //_fakeTransponderReceiver.   
        }

       [Test]
       public void HorizontalVelocity()
       {
          List<Aircraft> testAircraftsList = new List<Aircraft>();
         
          testAircraftsList.Add(new Aircraft("ATR423",39045,12932,14000, (DateTime.ParseExact("20151006213456789", "yyyyMMddHHmmssfff",
             System.Globalization.CultureInfo.InvariantCulture))));
         testAircraftsList.Add(new Aircraft("BCD123", 10005, 85890, 12000, (DateTime.ParseExact("20151006213456789", "yyyyMMddHHmmssfff",
             System.Globalization.CultureInfo.InvariantCulture)))); 
          testAircraftsList.Add(new Aircraft("XYZ987", 25059, 75654, 4000, (DateTime.ParseExact("20151006213456789", "yyyyMMddHHmmssfff",
             System.Globalization.CultureInfo.InvariantCulture))));

          List<Aircraft> newtestAircraftsList = new List<Aircraft>();
          newtestAircraftsList.Add(new Aircraft("ATR423", 45000, 15940, 16000, (DateTime.ParseExact("20151006214356895", "yyyyMMddHHmmssfff",
             System.Globalization.CultureInfo.InvariantCulture))));
          newtestAircraftsList.Add(new Aircraft("ACD123", 10005, 85890, 12000, (DateTime.ParseExact("20151006213456789", "yyyyMMddHHmmssfff",
             System.Globalization.CultureInfo.InvariantCulture))));
          newtestAircraftsList.Add(new Aircraft("XYZ987", 30000, 80654, 8000, (DateTime.ParseExact("20151006214518566", "yyyyMMddHHmmssfff",
             System.Globalization.CultureInfo.InvariantCulture))));

          uut.currentAircrafts = testAircraftsList;

          uut.HorizontalVelocity(newtestAircraftsList);

          Assert.That(newtestAircraftsList[0].HorizontalVelocity, Is.EqualTo(540.106));
       }
   }

}
