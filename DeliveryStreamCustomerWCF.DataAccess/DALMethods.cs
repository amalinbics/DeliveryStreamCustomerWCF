// 2014.01.06 FSWW, Ramesh M Added VersionNo as input parameter in all methods For Versioning handling
// 2014.01.28  Ramesh M Added For Support Multi BOL ites added ExtSysTrxLine For CR#62038
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryStreamCustomerWCF.Utils;
using System.Data;
using System.Data.SqlClient;
using DeliveryStreamCustomerWCF.Entities;
using System.IO;
using System.Xml.Serialization;

namespace DeliveryStreamCustomerWCF.DataAccess
{
    /// <summary>
    /// DALMethods
    /// DALMethods class
    /// </summary>
    public class DALMethods
    {
        /// <summary>
        /// UpdateLoad
        /// Function to update the load records
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="loadNo">Load number</param>
        /// <param name="vehicleID">Vehicle ID</param>
        /// <param name="driverID">Driver ID</param>
        public static void UpdateLoad(ISession session, string loadNo, int vehicleID, int driverID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateLoad";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoadNo", loadNo);
            cmd.Parameters.AddWithValue("@VehicleID", vehicleID);
            cmd.Parameters.AddWithValue("@DriverID", driverID);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// UpdateLoadStatus
        /// Function to update the load status
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="loadNo">Load number</param>
        /// <param name="statusCode">Status code</param>
        public static void UpdateLoadStatus(ISession session, string loadNo, String statusCode, String companyID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateLoadStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoadNo", loadNo);
            cmd.Parameters.AddWithValue("@StatusCode", statusCode);
            cmd.Parameters.AddWithValue("@ClientId", companyID);
            cmd.ExecuteNonQuery();

        }

        /// <summary>
        /// UpdateFreightBreakdown
        /// Function to update the FreightBreakdown details
        /// </summary>
        /// <param name="companyID">Company ID</param>
        /// <param name="password">Password</param>
        /// <param name="VersionNo">VersionNo</param>
        public static void UpdateFreightBreakdown(ISession session, String companyID, Int32 SysTrxNo, Int32 FrtBrkdownline, char LegType, String OriginCity, String DestinationCity, String OriginState, String DestinationState, decimal CalcMiles, decimal ActualMiles, decimal OriginLat, decimal OriginLong, decimal DestLat, decimal DestLong, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateFreightBreakdownDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientID", companyID);
            cmd.Parameters.AddWithValue("@OrderSystrxno", SysTrxNo);
            cmd.Parameters.AddWithValue("@FrtBrkdownline", FrtBrkdownline);
            cmd.Parameters.AddWithValue("@LegType", LegType);
            cmd.Parameters.AddWithValue("@OriginCity", OriginCity);
            cmd.Parameters.AddWithValue("@DestinationCity", DestinationCity);
            cmd.Parameters.AddWithValue("@OriginState", OriginState);
            cmd.Parameters.AddWithValue("@DestinationState", DestinationState);
            cmd.Parameters.AddWithValue("@CalcMiles", CalcMiles);
            cmd.Parameters.AddWithValue("@ActualMiles", ActualMiles);
            cmd.Parameters.AddWithValue("@OriginLat", OriginLat);
            cmd.Parameters.AddWithValue("@OriginLong", OriginLong);
            cmd.Parameters.AddWithValue("@DestLat", DestLat);
            cmd.Parameters.AddWithValue("@DestLong", DestLong);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// UpdateOrderStatus
        /// Function to update the order status
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="SysTrxNo">System transaction number</param>
        /// <param name="statusCode">Status code</param>z
        public static void UpdateOrderStatus(ISession session, decimal SysTrxNo, String statusCode,String companyID, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateOrderStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SysTrxNo", SysTrxNo);
            cmd.Parameters.AddWithValue("@StatusCode", statusCode);
            cmd.Parameters.AddWithValue("@ClientId", companyID);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// UpdateShipment
        /// Function to update the shipment details
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="shipmentReq">ShipmentRequest object</param>
        /// <returns>ShipmentResponse object</returns>
        public static ShipmentResponse UpdateShipment(ISession session, ShipmentRequest shipmentReq, String VersionNo = "")
        {
            string ComponentsXML = string.Empty;
            string innerExceptionMsg = string.Empty;
            string exceptionMsg = string.Empty;

            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<ShipmentRequestComponents>));
                xs.Serialize(sw, shipmentReq.Components);
                ComponentsXML = sw.ToString().Replace("utf-16", "utf-8");
            }

            List<string> strBOLQtyVarianceReason = shipmentReq.Components.Select(item => item.BOLQtyVarianceReason).Distinct().ToList();
            string BOLQtyVarianceReason = string.Empty;
            string netQtyVarianceReason = string.Empty;
            foreach (string reason in strBOLQtyVarianceReason)
            {
                if (!string.IsNullOrEmpty(reason))
                {
                    if (string.IsNullOrEmpty(BOLQtyVarianceReason))
                    {
                        BOLQtyVarianceReason = reason;
                    }
                    else
                    {
                        BOLQtyVarianceReason = BOLQtyVarianceReason + ", ";
                        BOLQtyVarianceReason = BOLQtyVarianceReason + reason;
                    }
                }
            }

            //shipmentReq.Components.Select(item=>item.NetQtyVarianceReason).DistinctBy(item => item.NetQtyVarianceReason).ToList();

            ShipmentResponse shipmentResponse = new ShipmentResponse();

            //SqlCommand cmdAudit = (SqlCommand)session.CreateCommand();
            //cmdAudit.CommandText = "Cloud_UpdateShipmentDetailsAuditLog";
            //cmdAudit.CommandType = CommandType.StoredProcedure;
            //cmdAudit.Parameters.AddWithValue("@SysTrxNo", shipmentReq.SysTrxNo);
            //cmdAudit.Parameters.AddWithValue("@SysLineNo", shipmentReq.SysTrxLine);
            //cmdAudit.Parameters.AddWithValue("@ComponentsXML", ComponentsXML);
            //cmdAudit.Parameters.AddWithValue("@OrderLoadReviewEnabled", shipmentReq.OrderLoadReviewEnabled);
            //cmdAudit.ExecuteNonQuery();

            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateShipmentDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SysTrxNo", shipmentReq.SysTrxNo);
            cmd.Parameters.AddWithValue("@SysLineNo", shipmentReq.SysTrxLine);
            cmd.Parameters.AddWithValue("@ComponentsXML", ComponentsXML);
            cmd.Parameters.AddWithValue("@UserID", shipmentReq.UserID);
            cmd.Parameters.AddWithValue("@OrderLoadReviewEnabled", shipmentReq.OrderLoadReviewEnabled);
            cmd.Parameters.AddWithValue("@BOLQtyVarianceReason", BOLQtyVarianceReason);

            //2013.09.23 FSWW, Ramesh M Added For CR#60090 to push BolImage data to Ascend
            cmd.Parameters.AddWithValue("@BOLImage", shipmentReq.Components.Select(item => item.BOLImage).First());
            //2014.01.28  Ramesh M Added For Support Multi BOL ites added ExtSysTrxLine For CR#62038
            //cmd.Parameters.AddWithValue("@SupplierCode", shipmentReq.SupplierCode);
            //cmd.Parameters.AddWithValue("@SupplyPtCode", shipmentReq.SupplyPointCode);
            //cmd.Parameters.AddWithValue("@ExtSysLineNo", shipmentReq.ExtSysTrxLine);
            IDataReader dreader = null;
            try
            {
                dreader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                shipmentResponse = new ShipmentResponse();
                shipmentResponse.ErrorMessage = ex.Message + " - " + ex.StackTrace;
                Logging.LogError(ex);
                return shipmentResponse;
            }
            if (dreader != null)
            {
                while (dreader.Read())
                {
                    shipmentResponse.ShipDocSysTrxNo = Convert.ToDecimal(dreader["ShipDocSysTrxNo"]);
                    shipmentResponse.ShipDocSysTrxLine = Convert.ToInt32(dreader["ShipDocSysTrxLine"]);
                    shipmentResponse.OrderLoadReviewEnabled = Convert.ToString(dreader["OrderLoadReviewEnabled"]).Equals("Y", StringComparison.CurrentCultureIgnoreCase);
                    shipmentResponse.ErrorMessage = "";
                }
                dreader.Close();
            }

            if (!shipmentResponse.OrderLoadReviewEnabled && (shipmentResponse.ShipDocSysTrxNo <= 0 || shipmentResponse.ShipDocSysTrxLine <= 0))
            {
                shipmentResponse = null;
                //throw new ApplicationException(String.Format("ShipDocSysTrxNo returned 0. Unable to update shipment details for SysTrxNo = {0}, SysLineNo={1} and UserID={2}", shipmentReq.SysTrxNo, shipmentReq.SysTrxLine, shipmentReq.UserID));
            }
            return shipmentResponse;
        }

        /// <summary>
        /// Funciton to log shipment errors in table Cloud_ShipmentErrors
        /// </summary>
        /// <param name="shipmentReq">ShipmentRequest object</param>
        /// <param name="isError">error or warning</param>
        /// <param name="errorDetail">error details</param>
        /// <param name="errorMessage">error message</param>
        /// <param name="session">session object</param>
        public static void UpdateShipmentErrors(ShipmentRequest shipmentReq, Boolean isError, String errorDetail, String errorMessage, ISession session,String companyID, String VersionNo = "")
        {
            try
            {
                string ComponentsXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<ShipmentRequestComponents>));
                    xs.Serialize(sw, shipmentReq.Components);
                    ComponentsXML = sw.ToString().Replace("utf-16", "utf-8");
                }

                SqlCommand cmd = (SqlCommand)session.CreateCommand();
                cmd.CommandText = "Cloud_UpdateShipmentErrors";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SysTrxNo", shipmentReq.SysTrxNo);
                cmd.Parameters.AddWithValue("@SysLineNo", shipmentReq.SysTrxLine);
                cmd.Parameters.AddWithValue("@ComponentsXML", ComponentsXML);
                cmd.Parameters.AddWithValue("@UserID", shipmentReq.UserID);
                cmd.Parameters.AddWithValue("@ErrorOrWarning", isError);
                cmd.Parameters.AddWithValue("@ErrorDetails", errorDetail);
                cmd.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                cmd.ExecuteNonQuery();
                UpdateOrderStatus(session, shipmentReq.SysTrxNo, ApplicationConstants.ErrorStatusCode.Error_In_Shipment,companyID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
            }
        }

        /// <summary>
        /// Function to clear shipment errors on sucessfull shipment
        /// </summary>
        /// <param name="sysTrxNo">sysTrxNo</param>
        /// <param name="sysTrxLine">sysTrxLine</param>
        /// <param name="session">session object</param>
        public static void ClearShipmentErrors(Decimal sysTrxNo, Int32 sysTrxLine, ISession session, String VersionNo = "")
        {
            try
            {
                SqlCommand cmd = (SqlCommand)session.CreateCommand();
                cmd.CommandText = "Cloud_ClearShipmentErrors";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SysTrxNo", sysTrxNo);
                cmd.Parameters.AddWithValue("@SysLineNo", sysTrxLine);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
            }
        }

        /// <summary>
        /// UpdateDeliveryDetails
        /// Function to update the delivery details
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="deliveryReq">DeliveryRequest object</param>
        public static void UpdateDeliveryDetails(ISession session, DeliveryRequest deliveryReq, String VersionNo = "")
        {

            Logging.WriteToFile1("UpdateDeliveryDetails" + " " + deliveryReq.Items.Count);
            string ItemsXML = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<DeliveryRequestItems>));
                xs.Serialize(sw, deliveryReq.Items);
                ItemsXML = sw.ToString().Replace("utf-16", "utf-8");
            }

            //SqlCommand cmdAudit = (SqlCommand)session.CreateCommand();
            //cmdAudit.CommandText = "Cloud_UpdateDeliveryDetailsAuditLog";
            //cmdAudit.CommandType = CommandType.StoredProcedure;
            //cmdAudit.Parameters.AddWithValue("@SysTrxNo", deliveryReq.SysTrxNo);
            //cmdAudit.Parameters.AddWithValue("@ItemsXML", ItemsXML);
            //cmdAudit.ExecuteNonQuery();


            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateDeliveryDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SysTrxNo", deliveryReq.SysTrxNo);
            cmd.Parameters.AddWithValue("@ItemsXML", ItemsXML);
            cmd.Parameters.AddWithValue("@UserID", deliveryReq.UserID);
            cmd.Parameters.AddWithValue("@BOLWaitTime", deliveryReq.BOLWaitTime);
            cmd.Parameters.AddWithValue("@BOLWaitTimeTotal", deliveryReq.BOLWaitTimeTotal);
            cmd.Parameters.AddWithValue("@SiteWaitTime", deliveryReq.SiteWaitTime);
            cmd.Parameters.AddWithValue("@SiteWaitTime_Comment", deliveryReq.SiteWaitTime_Comment);
            cmd.Parameters.AddWithValue("@SiteWaitTime_Start", deliveryReq.SiteWaitTime_Start);
            cmd.Parameters.AddWithValue("@SiteWaitTime_End", deliveryReq.SiteWaitTime_End);
            cmd.Parameters.AddWithValue("@SplitLoad", deliveryReq.SplitLoad);
            cmd.Parameters.AddWithValue("@SplitLoad_Comment", deliveryReq.SplitLoad_Comment);
            cmd.Parameters.AddWithValue("@SplitDrop", deliveryReq.SplitDrop);
            cmd.Parameters.AddWithValue("@SplitDrop_Comment", deliveryReq.SplitDrop_Comment);
            cmd.Parameters.AddWithValue("@PumpOut", deliveryReq.PumpOut);
            cmd.Parameters.AddWithValue("@PumpOut_Comment", deliveryReq.PumpOut_Comment);
            cmd.Parameters.AddWithValue("@Diversion", deliveryReq.Diversion);
            cmd.Parameters.AddWithValue("@Diversion_Comment", deliveryReq.Diversion_Comment);
            cmd.Parameters.AddWithValue("@MinimumLoad", deliveryReq.MinimumLoad);
            cmd.Parameters.AddWithValue("@MinimumLoad_Comment", deliveryReq.MinimumLoad_Comment);
            cmd.Parameters.AddWithValue("@Other", deliveryReq.Other);
            cmd.Parameters.AddWithValue("@Other_Comment", deliveryReq.Other_Comment);
            cmd.Parameters.AddWithValue("@SignatureStatus", deliveryReq.SignatureStatus);
            cmd.Parameters.AddWithValue("@OrderLoadReviewEnabled", deliveryReq.OrderLoadReviewEnabled);
            //2013.09.13 FSWW, Ramesh M Added ForCR#60123 Adding Two parameters SignatureImage and SignatureDate
            cmd.Parameters.AddWithValue("@SignatureImage", deliveryReq.SignatureImage);
            cmd.Parameters.AddWithValue("@SignatureDateTime", deliveryReq.SignatureDateTime);
          
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Funciton to log delivery errors in table Cloud_DeliveryErrors 
        /// </summary>
        /// <param name="deliveryReq">DeliveryRequest object</param>
        /// <param name="isError">is error or warning</param>
        /// <param name="errorDetail">error details</param>
        /// <param name="errorMessage">error message</param>
        /// <param name="session">session object</param>
        public static void UpdateDeliveryErrors(DeliveryRequest deliveryReq, Boolean isError, String errorDetail, String errorMessage, ISession session,String companyID, String VersionNo = "")
        {
            try
            {
                string ItemsXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    XmlSerializer xs = new XmlSerializer(typeof(List<DeliveryRequestItems>));
                    xs.Serialize(sw, deliveryReq.Items);
                    ItemsXML = sw.ToString().Replace("utf-16", "utf-8");
                }


                SqlCommand cmd = (SqlCommand)session.CreateCommand();
                cmd.CommandText = "Cloud_UpdateDeliveryErrors";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SysTrxNo", deliveryReq.SysTrxNo);
                cmd.Parameters.AddWithValue("@ItemsXML", ItemsXML);
                cmd.Parameters.AddWithValue("@UserID", deliveryReq.UserID);
                cmd.Parameters.AddWithValue("@ErrorOrWarning", isError);
                cmd.Parameters.AddWithValue("@ErrorDetails", errorDetail);
                cmd.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                cmd.ExecuteNonQuery();
                UpdateOrderStatus(session, deliveryReq.SysTrxNo, ApplicationConstants.ErrorStatusCode.Error_In_Delivery, companyID);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
            }
        }

        /// <summary>
        /// Function to clear delivery errors on sucessfull delivery
        /// </summary>
        /// <param name="sysTrxNo">sysTrxNo</param>
        /// <param name="session">session obhect</param>
        public static void ClearDeliveryErrors(Decimal sysTrxNo, ISession session, String VersionNo = "")
        {
            try
            {
                SqlCommand cmd = (SqlCommand)session.CreateCommand();
                cmd.CommandText = "Cloud_ClearDeliveryErrors";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SysTrxNo", sysTrxNo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
            }
        }


        /// <summary>
        /// InsertEODInventoryProcess
        /// Function to update the EODInventoryProcess
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="SysTrxNo">System transaction number</param>
        /// <param name="statusCode">Status code</param>
        public static void InsertEODInventoryProcess(ISession session, string inToSite, string inSupplierCode, string inSupplyPtCode, string inProductCode, decimal inOrgQty, decimal inAvailableQty, decimal inNetQty, int inOrderSysTrxNo, int inOrderSysTrxLineNo, int invehicleID, int inDriverID, string inUserID, string inBOLNo, DateTime inBOLDtTm, string inBOLSessionID, string inOverShort, int inClientID)
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "InternalTransferOrderProcessing";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@inCompanyID", "01");
            cmd.Parameters.AddWithValue("@inToSite", inToSite);
            cmd.Parameters.AddWithValue("@inSupplierCode", inSupplierCode);
            cmd.Parameters.AddWithValue("@inSupplyPtCode", inSupplyPtCode);
            cmd.Parameters.AddWithValue("@inProductCode", inProductCode);
            cmd.Parameters.AddWithValue("@inOrgQty", inOrgQty);
            cmd.Parameters.AddWithValue("@inAvailableQty", inAvailableQty);
            cmd.Parameters.AddWithValue("@invehicleID", invehicleID);
            cmd.Parameters.AddWithValue("@inDriverID", inDriverID);
            cmd.Parameters.AddWithValue("@inUserID", inUserID);
            cmd.Parameters.AddWithValue("@inBOLNo", inBOLNo);
            cmd.Parameters.AddWithValue("@inBOLDtTm", inBOLDtTm);
            cmd.Parameters.AddWithValue("@inBOLSessionID", DBNull.Value);
            cmd.Parameters.AddWithValue("@inOverShort", inOverShort);
            cmd.Parameters.AddWithValue("@inClientID", inClientID);
            cmd.Parameters.AddWithValue("@inDiffQty", inNetQty);
            cmd.Parameters.AddWithValue("@inOrderSysTrxNo", inOrderSysTrxNo);
            cmd.Parameters.AddWithValue("@inOrderSysTrxLine", inOrderSysTrxLineNo);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// UpdateShipment
        /// Function to update the shipment details
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="shipmentReq">ShipmentRequest object</param>
        /// <returns>ShipmentResponse object</returns>
        public static string UpdateWagonShipment(ISession session, TWBOLDetails shipmentReq, String VersionNo = "")
        {
            string ComponentsXML = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<TWBOLDetails>));
                xs.Serialize(sw, shipmentReq.BOLItemDetails);
                ComponentsXML = sw.ToString().Replace("utf-16", "utf-8");
            }
            return ComponentsXML;
            //List<string> strBOLQtyVarianceReason = shipmentReq.BOLItemDetails.Select(item => item.Notes).Distinct().ToList();
            //string BOLQtyVarianceReason = string.Empty;
            //string netQtyVarianceReason = string.Empty;
            //foreach (string reason in strBOLQtyVarianceReason)
            //{
            //    if (!string.IsNullOrEmpty(reason))
            //    {
            //        if (string.IsNullOrEmpty(BOLQtyVarianceReason))
            //        {
            //            BOLQtyVarianceReason = reason;
            //        }
            //        else
            //        {
            //            BOLQtyVarianceReason = BOLQtyVarianceReason + ", ";
            //            BOLQtyVarianceReason = BOLQtyVarianceReason + reason;
            //        }
            //    }
            //}

            ////shipmentReq.Components.Select(item=>item.NetQtyVarianceReason).DistinctBy(item => item.NetQtyVarianceReason).ToList();

            //ShipmentResponse shipmentResponse = new ShipmentResponse();

            //SqlCommand cmd = (SqlCommand)session.CreateCommand();
            //cmd.CommandText = "Cloud_UpdateShipmentDetails";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@SysTrxNo", shipmentReq.SysTrxNo);
            //cmd.Parameters.AddWithValue("@SysLineNo", shipmentReq.SysTrxLine);
            //cmd.Parameters.AddWithValue("@ComponentsXML", ComponentsXML);
            //cmd.Parameters.AddWithValue("@UserID", shipmentReq.UserID);
            //cmd.Parameters.AddWithValue("@OrderLoadReviewEnabled", shipmentReq.OrderLoadReviewEnabled);
            //cmd.Parameters.AddWithValue("@BOLQtyVarianceReason", BOLQtyVarianceReason);

            ////2013.09.23 FSWW, Ramesh M Added For CR#60090 to push BolImage data to Ascend
            //cmd.Parameters.AddWithValue("@BOLImage", shipmentReq.Components.Select(item => item.BOLImage).First());
            ////2014.01.28  Ramesh M Added For Support Multi BOL ites added ExtSysTrxLine For CR#62038
            ////cmd.Parameters.AddWithValue("@SupplierCode", shipmentReq.SupplierCode);
            ////cmd.Parameters.AddWithValue("@SupplyPtCode", shipmentReq.SupplyPointCode);
            ////cmd.Parameters.AddWithValue("@ExtSysLineNo", shipmentReq.ExtSysTrxLine);
            //IDataReader dreader = null;
            //try
            //{
            //    dreader = cmd.ExecuteReader();
            //}
            //catch (Exception ex)
            //{
            //    Logging.LogError(ex);
            //}
            //if (dreader != null)
            //{
            //    while (dreader.Read())
            //    {
            //        shipmentResponse.ShipDocSysTrxNo = Convert.ToDecimal(dreader["ShipDocSysTrxNo"]);
            //        shipmentResponse.ShipDocSysTrxLine = Convert.ToInt32(dreader["ShipDocSysTrxLine"]);
            //        shipmentResponse.OrderLoadReviewEnabled = Convert.ToString(dreader["OrderLoadReviewEnabled"]).Equals("Y", StringComparison.CurrentCultureIgnoreCase);
            //    }
            //    dreader.Close();
            //}

            //if (!shipmentResponse.OrderLoadReviewEnabled && (shipmentResponse.ShipDocSysTrxNo <= 0 || shipmentResponse.ShipDocSysTrxLine <= 0))
            //{
            //    shipmentResponse = null;
            //    throw new ApplicationException(String.Format("ShipDocSysTrxNo returned 0. Unable to update shipment details for SysTrxNo = {0}, SysLineNo={1} and UserID={2}", shipmentReq.SysTrxNo, shipmentReq.SysTrxLine, shipmentReq.UserID));
            //}
            //return shipmentResponse;
        }


        /// <summary>
        /// Function to Update the BOLImage
        /// </summary>
        /// <param name="BOLImage">BOLImage</param>
        /// <param name="sysTrxNo">sysTrxNo</param>
        public static void UpdateBOLImage(ISession session, byte[] BOLImage, byte[] BOLImagePdf, int SystrxNo, string Bolno, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateBOLImage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BOLImage", BOLImage);
            cmd.Parameters.AddWithValue("@BOLImagePdf", BOLImagePdf);
            cmd.Parameters.AddWithValue("@SysTrxNo", SystrxNo);
            cmd.Parameters.AddWithValue("@BOLNo", Bolno);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Function to Update the Order PONo
        /// </summary>
        /// <param name="BOLImage">OrderNo</param>
        /// <param name="sysTrxNo">SystrxNo</param>
        /// <param name="sysTrxNo">PoNo</param>
        public static void UpdatePONo(ISession session, string OrderNo, int SystrxNo, string PONo, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateOrderPONo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderNo", OrderNo);
            cmd.Parameters.AddWithValue("@SysTrxNo", SystrxNo);
            cmd.Parameters.AddWithValue("@PONo", PONo);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Function to Update the Undo Ship Doc
        /// </summary>
        /// <param name="sysTrxNo">SystrxNo</param>
        /// <param name="inStatus">inStatus</param>
        public static void UpdateUndoShipDoc(ISession session, int SystrxNo, char inStatus, String VersionNo = "")
        {
            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "ShipDocUpdStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inSysTrxNo", SystrxNo);
            cmd.Parameters.AddWithValue("@inNewStatus", 'A');
            cmd.ExecuteNonQuery();
        }

        #region RejectedNotes

        public static void InsertRejectedLoad(ISession session, string loadNumber, string rejectedNote, string customerId)
        {
            int isUpdated = 0;
            SqlConnection con = new SqlConnection(session.ConnectionString);
            try
            {
                SqlCommand cmd = (SqlCommand)session.CreateCommand();
                cmd.CommandText = "UpdateRejectedNotes";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LoadNo", loadNumber);
                cmd.Parameters.AddWithValue("@RejectedNote", rejectedNote);
                cmd.Parameters.AddWithValue("@ClientId", customerId);
                con.Open();
                isUpdated = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }
            finally
            {
                con.Close();
            }

        }
        #endregion


        #region DeliveryNotes

        public static int UpdateDeliveryNotes(ISession session, string sysTrsNo, string DeliveryNote)
        {
            int isUpdated = 0;
            SqlConnection con = new SqlConnection(session.ConnectionString);
            try
            {
                SqlCommand cmd = (SqlCommand)session.CreateCommand();
                cmd.CommandText = "UpdateDeliveryNotes";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SysTrsNo", sysTrsNo);
                cmd.Parameters.AddWithValue("@DeliveryNotes", DeliveryNote);
                con.Open();
                isUpdated = cmd.ExecuteNonQuery();
                //if (isUpdated > 0)
                //    Logging.WriteToFile1(string.Format("UpdateDeliveryNotes : LoadNo={0} is updated", sysTrsNo));
                //else
                //    Logging.WriteToFile1(string.Format("UpdateDeliveryNotes : LoadNo={0} is not updated", sysTrsNo));
            }
            catch (Exception ex)
            {
                Logging.WriteToFile1(string.Format("UpdateDeliveryNotes : {0}", ex.Message));
            }
            finally
            {
                con.Close();
            }
            return isUpdated;
        }

        #endregion

        /// <summary>
        /// UpdateDeliveryDetails
        /// Function to update the delivery details
        /// </summary>
        /// <param name="session">Session object</param>
        /// <param name="deliveryReq">DeliveryRequest object</param>
        public static void UpdateBOLWaitTimeDetails(ISession session, string ClientID, decimal SysTrxNo, string BOLNo, DateTime BOLWaitTimeStart, DateTime BOLWaitTimeEnd, string BOLWaitTimeComment, String VersionNo = "")
        {

            SqlCommand cmd = (SqlCommand)session.CreateCommand();
            cmd.CommandText = "Cloud_UpdateBOLWaitTimeDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientID", ClientID);
            cmd.Parameters.AddWithValue("@SysTrxNo", SysTrxNo);
            cmd.Parameters.AddWithValue("@BOLNo", BOLNo);
            cmd.Parameters.AddWithValue("@BOLWaitTimeStart", BOLWaitTimeStart);
            cmd.Parameters.AddWithValue("@BOLWaitTimeEnd", BOLWaitTimeEnd);
            cmd.Parameters.AddWithValue("@BOLWaitTimeComment", BOLWaitTimeComment);

            cmd.ExecuteNonQuery();
        }
    }
}
