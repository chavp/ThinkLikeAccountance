using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Accounting.Tests.Models;

namespace Models.Accounting.Tests
{
    [TestClass]
    public class ThinkLikeAccountant
    {
        List<Account> acctList = new List<Account>();
        // รหัสบัญชี: http://www.pw.ac.th/emedia/media/tech/accounting1/lesson4.php
        [TestInitialize]
        public void Setup()
        {
            var cashAcct = new Account
            {
                ID = 1,
                AccountType = AccountType.Assets,
                Code = "101",
                Description = "บัญชีเงินสด",
            };

            var bondAcct = new Account
            {
                ID = 2,
                AccountType = AccountType.Assets,
                Code = "102",
                Description = "บัญชีหุ้น",
            };

            var ownerAcct = new Account
            {
                ID = 3,
                AccountType = AccountType.Assets,
                Code = "301",
                Description = "บัญชีส่วนของเจ้าของ",
            };

            var goodsAcct = new Account
            {
                ID = 4,
                AccountType = AccountType.Assets,
                Code = "121",
                Description = "บัญชีสินค้าคงเหลือ",
            };

            var liaAcct = new Account
            {
                ID = 5,
                AccountType = AccountType.Liabilities,
                Code = "201",
                Description = "บัญชีเจ้าหนี้",
            };

            var revAcct = new Account
            {
                ID = 6,
                AccountType = AccountType.Equity,
                Code = "311",
                Description = "บัญชีรายรับ",
            };

            var costAcct = new Account
            {
                ID = 7,
                AccountType = AccountType.Equity,
                Code = "321",
                Description = "บัญชีต้นทุนขาย",
            };

            var receivableAcct = new Account
            {
                ID = 8,
                AccountType = AccountType.Assets,
                Code = "111",
                Description = "บัญชีลูกหนี้",
            };

            acctList.Add(cashAcct);
            acctList.Add(bondAcct);
            acctList.Add(ownerAcct);
            acctList.Add(goodsAcct);
            acctList.Add(liaAcct);
            acctList.Add(revAcct);
            acctList.Add(costAcct);
            acctList.Add(receivableAcct);
        }

        private void BuildOpeningOwnerInvestCash1000(List<AccountingTransaction> tranEntries)
        {
            var cashAcct = acctList.Where(x => x.Code == "101").Single();
            var bondAcct = acctList.Where(x => x.Code == "102").Single();
            var ownerAcct = acctList.Where(x => x.Code == "301").Single();

            var newTranCode = "IV-1";
            var entry1 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = cashAcct,
                Entry = new Entry
                {
                    ID = 1,
                    Amount = 1000,
                    Description = "เจ้าของนำเงินสดมาลงทุน",
                    EntryType = EntryType.Debit,
                }
            };
            var entry2 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = cashAcct,
                Entry = new Entry
                {
                    ID = 2,
                    Amount = 1000,
                    Description = "ซื้อหุ้น",
                    EntryType = EntryType.Credit,
                }
            };

            var entry3 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = bondAcct,
                Entry = new Entry
                {
                    ID = 3,
                    Amount = 1000,
                    Description = "ซื้อหุ้น",
                    EntryType = EntryType.Debit,
                }
            };

            var entry4 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = ownerAcct,
                Entry = new Entry
                {
                    ID = 4,
                    Amount = 1000,
                    Description = "นำเงินสดมาลงทุน",
                    EntryType = EntryType.Credit,
                }
            };

            tranEntries.Add(entry1);
            tranEntries.Add(entry2);
            tranEntries.Add(entry3);
            tranEntries.Add(entry4);
        }
        private void Buildซื้อสินค้าเป็นเงินเชื่อ10000บาท(List<AccountingTransaction> tranEntries)
        {
            var goodsAcct = acctList.Where(x => x.Code == "121").Single();
            var liaAcct = acctList.Where(x => x.Code == "201").Single();

            var newTranCode = "IV-2";
            var entry1 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = goodsAcct,
                Entry = new Entry
                {
                    ID = 5,
                    Amount = 10000,
                    Description = "ซื้อสินค้า 10 ชิ้น",
                    EntryType = EntryType.Debit,
                }
            };
            var entry2 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = liaAcct,
                Entry = new Entry
                {
                    ID = 6,
                    Amount = 10000,
                    Description = "กู้ยืมซื้อสินค้า",
                    EntryType = EntryType.Credit,
                }
            };

            tranEntries.Add(entry1);
            tranEntries.Add(entry2);

        }
        private void Buildขายสินค้าได้2ชิ้นรับเงินสดมา3000บาท(List<AccountingTransaction> tranEntries)
        {
            var revAcct = acctList.Where(x => x.Code == "311").Single();
            var cashAcct = acctList.Where(x => x.Code == "101").Single();

            var costAcct = acctList.Where(x => x.Code == "321").Single();
            var goodsAcct = acctList.Where(x => x.Code == "121").Single();

            var newTranCode = "REV-1";
            // บันทึกการขาย
            var entry1 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = cashAcct,
                Entry = new Entry
                {
                    ID = 7,
                    Amount = 3000,
                    Description = "เงินสดรับ",
                    EntryType = EntryType.Debit,
                }
            };
            var entry2 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = revAcct,
                Entry = new Entry
                {
                    ID = 8,
                    Amount = 3000,
                    Description = "ขายสินค้าได้ 2 ชิ้น",
                    EntryType = EntryType.Credit,
                }
            };

            // บันทึกปรับสินค้าคงคลัง
            var entry3 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = costAcct,
                Entry = new Entry
                {
                    ID = 9,
                    Amount = 2000,
                    Description = "ต้นทุนสินค้า 2 ชิ้น",
                    EntryType = EntryType.Debit,
                }
            };
            var entry4 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = goodsAcct,
                Entry = new Entry
                {
                    ID = 10,
                    Amount = 2000,
                    Description = "ต้นทุนสินค้า 2 ชิ้น",
                    EntryType = EntryType.Credit,
                }
            };

            tranEntries.Add(entry1);
            tranEntries.Add(entry2);
            tranEntries.Add(entry3);
            tranEntries.Add(entry4);
        }
        private void Buildขายสินค้าได้3ชิ้นผ่านระบบออนไลย์รับเงินผ่านบัตรเครดิตมา4600บาท(List<AccountingTransaction> tranEntries)
        {
            var revAcct = acctList.Where(x => x.Code == "311").Single();
            var recAcct = acctList.Where(x => x.Code == "111").Single();
            var costAcct = acctList.Where(x => x.Code == "321").Single();
            var goodsAcct = acctList.Where(x => x.Code == "121").Single();
            var liaAcct = acctList.Where(x => x.Code == "201").Single();

            var newTranCode = "REV-2";
            var entry1 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = recAcct,
                Entry = new Entry
                {
                    ID = 11,
                    Amount = 4600,
                    Description = "ขายสินค้า 3 ชิ้น บวก ค่าขนส่ง",
                    EntryType = EntryType.Debit,
                }
            };
            var entry2 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = revAcct,
                Entry = new Entry
                {
                    ID = 12,
                    Amount = 4500,
                    Description = "ขายสินค้า 3 ชิ้น",
                    EntryType = EntryType.Credit,
                }
            };
            var entry3 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = revAcct,
                Entry = new Entry
                {
                    ID = 13,
                    Amount = 100,
                    Description = "ค่าขนส่ง",
                    EntryType = EntryType.Credit,
                }
            };
            var entry4 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = costAcct,
                Entry = new Entry
                {
                    ID = 14,
                    Amount = 3000,
                    Description = "ขายสินค้า 3 ชิ้น",
                    EntryType = EntryType.Debit,
                }
            };
            var entry5 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = costAcct,
                Entry = new Entry
                {
                    ID = 15,
                    Amount = 100,
                    Description = "ค่าขนส่ง",
                    EntryType = EntryType.Debit,
                }
            };
            var entry6 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = goodsAcct,
                Entry = new Entry
                {
                    ID = 16,
                    Amount = 3000,
                    Description = "ขายสินค้า 3 ชิ้น",
                    EntryType = EntryType.Credit,
                }
            };
            var entry7 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = liaAcct,
                Entry = new Entry
                {
                    ID = 17,
                    Amount = 100,
                    Description = "ค่าขนส่ง",
                    EntryType = EntryType.Credit,
                }
            };

            tranEntries.Add(entry1);
            tranEntries.Add(entry2);
            tranEntries.Add(entry3);
            tranEntries.Add(entry4);
            tranEntries.Add(entry5);
            tranEntries.Add(entry6);
            tranEntries.Add(entry7);
        }

        [TestMethod]
        public void OpeningOwnerInvestCash1000()
        {
            var tranEntry = new List<AccountingTransaction>();
            BuildOpeningOwnerInvestCash1000(tranEntry);
            Assert.AreEqual(4, tranEntry.Count);

            // Assert total balance
            var debit = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit).Select(x => x.Entry.Amount).Sum();
            var credit = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit).Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(0, debit - credit);
        }

        [TestMethod]
        public void ซื้อสินค้าเป็นเงินเชื่อ10000บาท()
        {
            var tranEntry = new List<AccountingTransaction>();
            Buildซื้อสินค้าเป็นเงินเชื่อ10000บาท(tranEntry);

            Assert.AreEqual(2, tranEntry.Count);

            // Assert total balance
            var debit = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit).Select(x => x.Entry.Amount).Sum();
            var credit = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit).Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(0, debit - credit);
        }

        [TestMethod]
        public void ขายสินค้าได้2ชิ้นรับเงินสดมา3000บาท()
        {
            var tranEntry = new List<AccountingTransaction>();
            Buildซื้อสินค้าเป็นเงินเชื่อ10000บาท(tranEntry);
            Buildขายสินค้าได้2ชิ้นรับเงินสดมา3000บาท(tranEntry);

            Assert.AreEqual(6, tranEntry.Count);

            // Assert total balance 
            var debit = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit).Select(x => x.Entry.Amount).Sum();
            var credit = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit).Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(0, debit - credit);

            // Assert ดุล บัญชีสินค้าคงเหลือ ต้องเท่ากับ 8000 
            var goodsBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.Code == "121").Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.Code == "121").Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(8000, goodsBalance);

            // Assert profit
            var revenueBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.Code == "311").Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.Code == "311").Select(x => x.Entry.Amount).Sum();

            var costBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.Code == "321").Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.Code == "321").Select(x => x.Entry.Amount).Sum();

            var netIncome = revenueBalance - costBalance;
            Assert.AreEqual(1000, netIncome);
        }

        [TestMethod]
        public void ขายสินค้าได้3ชิ้นผ่านระบบออนไลย์รับเงินผ่านบัตรเครดิตมา4600บาท()
        {
            var tranEntry = new List<AccountingTransaction>();
            Buildซื้อสินค้าเป็นเงินเชื่อ10000บาท(tranEntry);
            Buildขายสินค้าได้2ชิ้นรับเงินสดมา3000บาท(tranEntry);
            Buildขายสินค้าได้3ชิ้นผ่านระบบออนไลย์รับเงินผ่านบัตรเครดิตมา4600บาท(tranEntry);

            Assert.AreEqual(13, tranEntry.Count);

            // Assert total balance 
            var debit = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit).Select(x => x.Entry.Amount).Sum();
            var credit = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit).Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(0, debit - credit);

            // Assert ดุล บัญชีสินค้าคงเหลือ ต้องเท่ากับ 5000 (เพราะขายไป 5 ชิ้นแล้ว) 
            var goodsBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.Code == "121").Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.Code == "121").Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(5000, goodsBalance);

            // Assert profit
            var revenueBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.Code == "311").Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.Code == "311").Select(x => x.Entry.Amount).Sum();

            var costBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.Code == "321").Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.Code == "321").Select(x => x.Entry.Amount).Sum();

            var netIncome = revenueBalance - costBalance;
            Assert.AreEqual(2500, netIncome);
        }

        [TestMethod]
        public void SaleSummary()
        {
            var tranEntry = new List<AccountingTransaction>();
            Buildซื้อสินค้าเป็นเงินเชื่อ10000บาท(tranEntry);
            Buildขายสินค้าได้2ชิ้นรับเงินสดมา3000บาท(tranEntry);
            Buildขายสินค้าได้3ชิ้นผ่านระบบออนไลย์รับเงินผ่านบัตรเครดิตมา4600บาท(tranEntry);

            var revenueBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.Code == "311").Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.Code == "311").Select(x => x.Entry.Amount).Sum();

            var costBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.Code == "321").Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.Code == "321").Select(x => x.Entry.Amount).Sum();

            Console.WriteLine("มูลค่าขายรวม: {0} บาท", revenueBalance);
            Console.WriteLine("ต้นทุนขายรวม: {0} บาท", costBalance);
            Console.WriteLine("กำไรสุัทธิ: {0} บาท", revenueBalance - costBalance);
        }

        private void Buildลูกค้านำเงินสดมาฝาก100000บาท(List<AccountingTransaction> tranEntries)
        {
            var cashAcct = acctList.Where(x => x.Code == "101").Single();
            var liaAcct = acctList.Where(x => x.Code == "201").Single();

            var newTranCode = "DEP-1";
            var entry1 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = cashAcct,
                Entry = new Entry
                {
                    ID = 18,
                    Amount = 100000,
                    Description = "ฝากเงิน",
                    EntryType = EntryType.Debit,
                }
            };
            var entry2 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = liaAcct,
                Entry = new Entry
                {
                    ID = 19,
                    Amount = 100000,
                    Description = "ฝากเงิน",
                    EntryType = EntryType.Credit,
                }
            };

            tranEntries.Add(entry1);
            tranEntries.Add(entry2);
        }
        private void Buildธนาคารปล่อยกู้ให้ลูกค้าจำนวน100000บาทโอนเงินเข้าบัญชีลูกค้า(List<AccountingTransaction> tranEntries)
        {
            var receivableAcct = acctList.Where(x => x.Code == "111").Single();
            var liaAcct = acctList.Where(x => x.Code == "201").Single();

            var newTranCode = "LON-1";
            var entry1 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = receivableAcct,
                Entry = new Entry
                {
                    ID = 20,
                    Amount = 100000,
                    Description = "กู้เงิน",
                    EntryType = EntryType.Debit,
                }
            };
            var entry2 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = liaAcct,
                Entry = new Entry
                {
                    ID = 21,
                    Amount = 100000,
                    Description = "โอนเงินเข้าบัญชี",
                    EntryType = EntryType.Credit,
                }
            };

            tranEntries.Add(entry1);
            tranEntries.Add(entry2);
        }
        private void Buildธนาคารจ่ายดอกเบี้ยเงินฝากประจำเดือน1จำนวน1000บาทตัดผ่านระบบ(List<AccountingTransaction> tranEntries)
        {
            var costAcct = acctList.Where(x => x.Code == "321").Single();
            var liaAcct = acctList.Where(x => x.Code == "201").Single();
            var newTranCode = "INT-1";
            var entry1 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = costAcct,
                Entry = new Entry
                {
                    ID = 22,
                    Amount = 1000,
                    Description = "ดอกเบี้ยเงินฝาก",
                    EntryType = EntryType.Debit,
                }
            };
            var entry2 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = liaAcct,
                Entry = new Entry
                {
                    ID = 23,
                    Amount = 1000,
                    Description = "ดอกเบี้ยเงินฝาก",
                    EntryType = EntryType.Credit,
                }
            };

            tranEntries.Add(entry1);
            tranEntries.Add(entry2);
        }
        private void Buildธนาคารรับชำระเงินจากลูกค้าเงินกู้งวดที่1คิดเป็นเงินต้น8000บาทและดอกเบี้ย2000บาทด้วยเงินสด(List<AccountingTransaction> tranEntries)
        {
            var cashAcct = acctList.Where(x => x.Code == "101").Single();
            var revAcct = acctList.Where(x => x.Code == "311").Single();
            var costAcct = acctList.Where(x => x.Code == "321").Single();
            var receivableAcct = acctList.Where(x => x.Code == "111").Single();
            var newTranCode = "RELON-1";
            var entry1 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = cashAcct,
                Entry = new Entry
                {
                    ID = 24,
                    Amount = 10000,
                    Description = "ชำระเงินกู้งวด 1",
                    EntryType = EntryType.Debit,
                }
            };
            var entry2 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = revAcct,
                Entry = new Entry
                {
                    ID = 25,
                    Amount = 8000,
                    Description = "เงินต้น",
                    EntryType = EntryType.Credit,
                }
            };
            var entry3 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = revAcct,
                Entry = new Entry
                {
                    ID = 26,
                    Amount = 2000,
                    Description = "ดอกเบี้ย",
                    EntryType = EntryType.Credit,
                }
            };

            var entry4 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = costAcct,
                Entry = new Entry
                {
                    ID = 27,
                    Amount = 8000,
                    Description = "หักยอดลูกหนี้",
                    EntryType = EntryType.Debit,
                }
            };
            var entry5 = new AccountingTransaction
            {
                TransactionCode = newTranCode,
                Account = receivableAcct,
                Entry = new Entry
                {
                    ID = 28,
                    Amount = 8000,
                    Description = "หักยอดลูกหนี้",
                    EntryType = EntryType.Credit,
                }
            };

            tranEntries.Add(entry1);
            tranEntries.Add(entry2);
            tranEntries.Add(entry3);
            tranEntries.Add(entry4);
            tranEntries.Add(entry5);
        }

        [TestMethod]
        public void ลูกค้านำเงินสดมาฝาก100000บาท()
        {
            var tranEntry = new List<AccountingTransaction>();
            Buildลูกค้านำเงินสดมาฝาก100000บาท(tranEntry);

            Assert.AreEqual(2, tranEntry.Count);

            var assetBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.AccountType == AccountType.Assets ).Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.AccountType == AccountType.Assets).Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(100000, assetBalance, "ตรวจสอบสินทรัพย์รวม");

            var liaBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.AccountType == AccountType.Liabilities).Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.AccountType == AccountType.Liabilities).Select(x => x.Entry.Amount).Sum();

            // https://sites.google.com/site/jira8788/cheapter3/liquidity
            Console.WriteLine("อัตราส่วนเงินทุนหมุนเวียน (Current Ratio): {0}", (assetBalance / liaBalance) );
        }

        [TestMethod]
        public void ธนาคารปล่อยกู้ให้ลูกค้าจำนวน100000บาทโอนเงินเข้าบัญชีลูกค้า()
        {
            var tranEntry = new List<AccountingTransaction>();
            Buildลูกค้านำเงินสดมาฝาก100000บาท(tranEntry);
            Buildธนาคารปล่อยกู้ให้ลูกค้าจำนวน100000บาทโอนเงินเข้าบัญชีลูกค้า(tranEntry);

            Assert.AreEqual(4, tranEntry.Count);

            var assetBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.AccountType == AccountType.Assets).Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.AccountType == AccountType.Assets).Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(200000, assetBalance, "ตรวจสอบสินทรัพย์รวม");

            var liaBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.AccountType == AccountType.Liabilities).Select(x => x.Entry.Amount).Sum() -
               tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.AccountType == AccountType.Liabilities).Select(x => x.Entry.Amount).Sum();

            Console.WriteLine("อัตราส่วนเงินทุนหมุนเวียน (Current Ratio): {0}", (assetBalance / liaBalance) );
        }

        [TestMethod]
        public void ธนาคารจ่ายดอกเบี้ยเงินฝากประจำเดือน1จำนวน1000บาทตัดผ่านระบบ()
        {
            var tranEntry = new List<AccountingTransaction>();
            Buildลูกค้านำเงินสดมาฝาก100000บาท(tranEntry);
            Buildธนาคารปล่อยกู้ให้ลูกค้าจำนวน100000บาทโอนเงินเข้าบัญชีลูกค้า(tranEntry);
            Buildธนาคารจ่ายดอกเบี้ยเงินฝากประจำเดือน1จำนวน1000บาทตัดผ่านระบบ(tranEntry);

            Assert.AreEqual(6, tranEntry.Count);

            var assetBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.AccountType == AccountType.Assets).Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.AccountType == AccountType.Assets).Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(200000, assetBalance, "ตรวจสอบสินทรัพย์รวม");

            var liaBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.AccountType == AccountType.Liabilities).Select(x => x.Entry.Amount).Sum() -
               tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.AccountType == AccountType.Liabilities).Select(x => x.Entry.Amount).Sum();

            Console.WriteLine("อัตราส่วนเงินทุนหมุนเวียน (Current Ratio): {0}", (assetBalance / liaBalance) );
        }

        [TestMethod]
        public void ธนาคารรับชำระเงินจากลูกค้าเงินกู้งวดที่1คิดเป็นเงินต้น8000บาทและดอกเบี้ย2000บาทด้วยเงินสด()
        {
            var tranEntry = new List<AccountingTransaction>();
            Buildลูกค้านำเงินสดมาฝาก100000บาท(tranEntry);
            Buildธนาคารปล่อยกู้ให้ลูกค้าจำนวน100000บาทโอนเงินเข้าบัญชีลูกค้า(tranEntry);
            Buildธนาคารจ่ายดอกเบี้ยเงินฝากประจำเดือน1จำนวน1000บาทตัดผ่านระบบ(tranEntry);
            Buildธนาคารรับชำระเงินจากลูกค้าเงินกู้งวดที่1คิดเป็นเงินต้น8000บาทและดอกเบี้ย2000บาทด้วยเงินสด(tranEntry);

            Assert.AreEqual(11, tranEntry.Count);

            var assetBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.AccountType == AccountType.Assets).Select(x => x.Entry.Amount).Sum() -
                tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.AccountType == AccountType.Assets).Select(x => x.Entry.Amount).Sum();

            Assert.AreEqual(202000, assetBalance, "ตรวจสอบสินทรัพย์รวม");

            var liaBalance = tranEntry.Where(x => x.Entry.EntryType == EntryType.Credit && x.Account.AccountType == AccountType.Liabilities).Select(x => x.Entry.Amount).Sum() -
               tranEntry.Where(x => x.Entry.EntryType == EntryType.Debit && x.Account.AccountType == AccountType.Liabilities).Select(x => x.Entry.Amount).Sum();

            Console.WriteLine("อัตราส่วนเงินทุนหมุนเวียน (Current Ratio): {0}", (assetBalance / liaBalance) );
        }
    }
}
