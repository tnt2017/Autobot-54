using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBot54
{
    public class Order
    {
        public string number { get; set; }
        public string internalNumber { get; set; }
        public string date { get; set; }
        public string comment { get; set; }
        public string userId { get; set; }
        public string dateUpdated { get; set; }
        public string profileId { get; set; }
        public string deliveryAddressId { get; set; }
        public string deliveryAddress { get; set; }
        public string deliveryOfficeId { get; set; }
        public object deliveryOffice { get; set; }
        public string deliveryTypeId { get; set; }
        public object deliveryType { get; set; }
        public string deliveryCost { get; set; }
        public string paymentTypeId { get; set; }
        public string shipmentDate { get; set; }
        public string debt { get; set; }
        public object basketId { get; set; }
        public object basketName { get; set; }
        public string paymentType { get; set; }
        public string userName { get; set; }
        public string userFullName { get; set; }
        public string userCode { get; set; }
        public string code { get; set; }
        public string managerId { get; set; }
        public string userEmail { get; set; }
        public string isDelete { get; set; }
        public object clientOrderNumber { get; set; }
        public Position[] positions { get; set; }
        public int positionsQuantity { get; set; }
        public float sum { get; set; }
        public object[] notes { get; set; }
    }

    public class Position
    {
        public string id { get; set; }
        public string statusCode { get; set; }
        public string comment { get; set; }
        public string commentAnswer { get; set; }
        public string routeId { get; set; }
        public string distributorId { get; set; }
        public string distributorName { get; set; }
        public string distributorType { get; set; }
        public string supplierCode { get; set; }
        public string dsRouteId { get; set; }
        public string itemKey { get; set; }
        public string quantity { get; set; }
        public string quantityFinal { get; set; }
        public float priceIn { get; set; }
        public float priceOut { get; set; }
        public float oldPriceOut { get; set; }
        public int priceRate { get; set; }
        public float priceInSiteCurrency { get; set; }
        public string currencyInId { get; set; }
        public string currencyOutId { get; set; }
        public string deadline { get; set; }
        public string deadlineMax { get; set; }
        public string status { get; set; }
        public string statusChangeDate { get; set; }
        public string number { get; set; }
        public string numberFix { get; set; }
        public string brand { get; set; }
        public string brandFix { get; set; }
        public string description { get; set; }
        public string dateUpdated { get; set; }
        public string isCanceled { get; set; }
        public string lineReference { get; set; }
        public string code { get; set; }
        public string distributorOrderId { get; set; }
        public string distributorOrderView { get; set; }
        public string weight { get; set; }
        public string volume { get; set; }
        public string isDelete { get; set; }
        public string noReturn { get; set; }
        public string locationsFullCodes { get; set; }
        public object articleCode { get; set; }
        public object garageCarId { get; set; }
    }



}
