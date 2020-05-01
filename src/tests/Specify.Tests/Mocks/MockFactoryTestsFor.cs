﻿using System;
using System.IO;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Mocks;

namespace Specify.Tests.Mocks
{
    [TestFixture]
    public abstract class MockFactoryTestsFor<T> where T : IMockFactory, new()
    {
        protected abstract string AssemblyName { get; }
        protected abstract string TypeName { get; }

        protected abstract IMockFactory CreateSut(IFileSystem fileSystem);

        [Test]
        public void should_not_throw_FileNotFoundException_if_Mock_assembly_not_referenced()
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.Load(Arg.Any<string>()).Returns(x => { throw new FileNotFoundException(); });
            Should.NotThrow(() => CreateSut(fileSystem));
        }

        [Test]
        public void should_return_Name_of_mocking_framework()
        {
            var sut = CreateSut(Substitute.For<IFileSystem>());
            sut.MockProviderName.ShouldBe(AssemblyName);
        }
    }
}
