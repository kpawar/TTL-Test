﻿using AddressProcessing.Address;
using AddressProcessing.Address.v1;
using NUnit.Framework;
using System;
using System.IO;

namespace AddressProcessing.Tests
{
    [TestFixture]
    public class AddressFileProcessorTests
    {
        private FakeMailShotService _fakeMailShotService;
        //make variable readlonly to dynamically set the current directory
        private readonly string TestInputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"test_data\contacts.csv");

        [SetUp]
        public void SetUp()
        {
            _fakeMailShotService = new FakeMailShotService();
        }

        [Test]
        public void Should_send_mail_using_mailshot_service()
        {
            var processor = new AddressFileProcessor(_fakeMailShotService);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            processor.Process(TestInputFile);
            watch.Stop();            
            System.Diagnostics.Debug.WriteLine($"ellapsed is {watch.Elapsed.Ticks}");
            Assert.That(_fakeMailShotService.Counter, Is.EqualTo(229));
        }

        internal class FakeMailShotService : IMailShot
        {
            internal int Counter { get; private set; }

            public void SendMailShot(string name, string address)
            {
                Counter++;
            }
        }
    }
}