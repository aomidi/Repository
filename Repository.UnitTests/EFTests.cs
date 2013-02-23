﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using Repository.EntityFramework;

namespace Repository.UnitTests
{
    internal class TestObject
    {
        //===============================================================
        [Key]
        public int ID { get; set; }
        //===============================================================
        public String Value { get; set; }
        //===============================================================
    }

    internal class TestContext : DbContext
    {
        //===============================================================
        public DbSet<TestObject> Objects { get; set; }
        //===============================================================
    }

    [TestFixture]
    internal class EFRepositoryTests
    {
        //===============================================================
        [Test]
        public void BatchInsertTest()
        {
            using (var repo = new EFRepository<TestContext, TestObject>(x => x.Objects, x => x.ID))
            {
                repo.RemoveAll(repo.Items.ToList().Select(x => new object[] { x.ID }));
                var objects = Enumerable.Range(0, 100).Select(x => new TestObject { ID = x, Value = x.ToString() }).ToList();
                repo.Store(objects);
            }
        }
        //===============================================================
    }
}