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

            Assert.That(uut.WithDataAircrafts.Count.Equals(1));
        }

        [Test]
        public void AirspaceCorrectXCoordinate()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.WithDataAircrafts[0].XCoordinate.Equals(85000));
        }

        [Test]
        public void AirspaceCorrectYCoordinate()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.WithDataAircrafts[0].YCoordinate.Equals(75654));
        }
        [Test]
        public void AirspaceCorrectAltitude()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.WithDataAircrafts[0].Altitude.Equals(4000));
        }

        [Test]
        public void AirspaceCorrectTimestamp()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.WithDataAircrafts[0].Timestamp.Equals(new DateTime(2015,10,06,21,34,56,789)));
        }

        [Test]
        public void AirspaceCorrectTag()
        {
            List<string> testData = new List<string>();
            testData.Add("XYZ987;85000;75654;4000;20151006213456789");

            // Act: Trigger the fake object to execute event invocation
            _fakeTransponderReceiver.TransponderDataReady
                += Raise.EventWith(this, new RawTransponderDataEventArgs(testData));

            Assert.That(uut.WithDataAircrafts[0].Tag.Equals("XYZ987"));
        }


        //[Test]
        //[TestCase(5,7)]
        //[TestCase(0,4)]
        //[TestCase(9,0)]
        //[TestCase(0,4)]
        //public void CalculateCompassCourse_ZCurrentAircraft_QNewAircraft_ResultsInQCurrentAircrafts(int Z, int Q)
        //{
        //    for (int i = 0; i < Z; i++)
        //    {
        //        uut.WithDataAircrafts.Add(new Aircraft("AAAAAA", i, i, i, DateTime.Now));
        //    }
        //    List<Aircraft> newAircraft = new List<Aircraft>();
        //    for (int i = 0; i < Q; i++)
        //    {
        //        newAircraft.Add(new Aircraft("BBBBBB", i, i, i, DateTime.Now));
        //    }

        //    uut.CalculateCompassCourse(newAircraft);

        //    Assert.That(uut.WithoutDataAircrafts.Count, Is.EqualTo(Q));
        //}



        [Test]
        [TestCase("AAA111", 10000, 10000, 1000, "AAA111", 10000, 40000, 1000, 0)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 10000, 40000, 1000, 180)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 20000, 10000, 1000, 90)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 50000, 10000, 1000, 270)]
        public void CalculateCompassCourse_AircraftTravelingNorthSouthEastWest(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            uut.WithDataAircrafts.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));

            List<Aircraft> newAircraft = new List<Aircraft>();
            newAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            uut.CalculateCompassCourse(newAircraft);

            Assert.That(newAircraft[0].CompassCourse, Is.EqualTo(course1));
        }


        [Test]
        [TestCase("AAA111", 10000, 5000, 1000, "AAA111", 10001, 85000, 1000, 0)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 10100, 70000, 1000, 1)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 10100, 10100, 1000, 45)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 70000, 10100, 1000, 89)]
        [TestCase("EAA111", 5000, 10000, 1000, "EAA111", 85000, 10001, 1000, 90)]
        public void CalculateCompassCourse_AircraftTravelingBetweenNorthAndEast(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            uut.WithDataAircrafts.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));

            // List<Aircraft> newAircraft = new List<Aircraft>();}}
            uut.WithoutDataAircrafts.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            uut.CalculateCompassCourse(uut.WithoutDataAircrafts);

            Assert.That(uut.WithoutDataAircrafts[0].CompassCourse, Is.EqualTo(course1));
        }

        [Test]
        //[TestCase("AAA111", 10000, 85000, 1000, "AAA111", 10001, 5000, 1000, 180)]
        [TestCase("BAA111", 10000, 60000, 1000, "BAA111", 10100, 50000, 1000, 91)]
        [TestCase("CAA111", 10000, 10000, 1000, "CAA111", 10100, 9900, 1000, 135)]
        [TestCase("DAA111", 60000, 10000, 1000, "DAA111", 70000, 9900, 1000, 179)]
        [TestCase("EAA111", 5000, 10000, 1000, "EAA111", 85000, 10001, 1000, 90)]
        public void CalculateCompassCourse_AircraftTravelingBetweenEastAndSouth(string t1, int x1, int y1, int a1, string t2, int x2, int y2, int a2, int course1)
        {
            uut.WithDataAircrafts.Add(new Aircraft(t1, x1, y1, a1, DateTime.Now));

            List<Aircraft> newAircraft = new List<Aircraft>();
            newAircraft.Add(new Aircraft(t2, x2, y2, a2, DateTime.Now));

            uut.CalculateCompassCourse(newAircraft);

            Assert.That(newAircraft[0].CompassCourse, Is.EqualTo(course1));
        }


       [Test]
       [TestCase("ATR423", 39045, 12932, 14000, "20151006213456789", "ATR423", 45000, 15940, 16000, "20151006214356895", 12.352)]
       [TestCase("SKF218",82000,21000,16300,"20191203213426980","SKF218",83000,19960,37000,"20191203214456990",2.2901)]
       [TestCase("EDB239", 10000, 60000, 1000, "20191203214726980", "EDB239", 20000, 80000, 1000, "20191203215356990", 57.334)]
      public void HorizontalVelocity(string t1, int x1, int y1, int a1, string ts1, string t2, int x2, int y2, int a2, string ts2, double velocity1)
       {
         uut.WithDataAircrafts.Add(new Aircraft(t1,x1,y1,a1, (DateTime.ParseExact(ts1, "yyyyMMddHHmmssfff",
            System.Globalization.CultureInfo.InvariantCulture))));

        uut.WithoutDataAircrafts.Add(new Aircraft(t2,x2,y2,a2, (DateTime.ParseExact(ts2, "yyyyMMddHHmmssfff",
             System.Globalization.CultureInfo.InvariantCulture))));
         
          uut.HorizontalVelocity(uut.WithoutDataAircrafts);

          Assert.That(uut.WithoutDataAircrafts[0].HorizontalVelocity, Is.EqualTo(velocity1).Within(00.01));
       }
   }

}
