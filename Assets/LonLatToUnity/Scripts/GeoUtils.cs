using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATGC.GEO
{
    public class GeoUtils
    {
        public const double OriginShift = 2 * Math.PI * 6378137 / 2.0; // 地球半径乘以π，用于Web墨卡托投影计算
        /// <summary>
        /// 地球半径，单位：米
        /// </summary>
        public const float EarthRadius = 6371000f; // 地球半径，单位：米
        /// <summary>
        /// 将经纬度坐标转换为Web墨卡托投影坐标
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static Vector2d LonLatToWebMercator(double lon, double lat)
        {
            // 经度转换
            double x = lon * OriginShift / 180.0;

            // 纬度转换
            //double latRad = lat * Math.PI / 180.0;
            double y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360.0)) / (Math.PI / 180.0);
            y = y * OriginShift / 180.0;

            return new Vector2d(x, y);
        }

        /// <summary>
        /// 将Web墨卡托投影坐标转换为经纬度坐标,缩放比例为1
        /// 注意：经纬度坐标和Web墨卡托投影坐标的坐标系不一样，Web墨卡托投影坐标的中心点在经线的正北方，经度范围为[-20037508.34, 20037508.34]，纬度范围为[-180, 85.0511287798066]
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="centerPoint">中心点</param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector2d GeoToWorldPositionLongLat(double lon, double lat, Vector2d centerPoint, float scale = 1)
        {
            var posx = lon * OriginShift / 180.0;
            var posy = Math.Log(Math.Tan((90 + lat) * Math.PI / 360.0)) / (Math.PI / 180.0);
            posy = posy * OriginShift / 180.0;
            return new Vector2d((posx - centerPoint.x) * scale, (posy - centerPoint.y) * scale);
        }

        public Vector2 GetLatLongToWorld2D(double latitude, double longitude)
        {
            // 将经纬度从度数转换为弧度
            //float转换为double，避免精度丢失

            double latRad = (Mathf.Deg2Rad * latitude);
            double lonRad = (Mathf.Deg2Rad * longitude);

            // 使用球坐标计算x, y, z值
            double x = EarthRadius * Math.Cos(latRad) * Math.Cos(lonRad);
            double y = EarthRadius * Math.Cos(latRad) * Math.Sin(lonRad);
            double z = EarthRadius * Math.Sin(latRad);

            return new Vector2((float)x, (float)z);
            //return new Vector3((float)x, 0, (float)z);
        }
    }
}