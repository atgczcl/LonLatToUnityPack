using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATGC.GEO
{
    public class GeoUtils
    {
        public const double OriginShift = 2 * Math.PI * 6378137 / 2.0; // ����뾶���ԦУ�����Webī����ͶӰ����
        /// <summary>
        /// ����뾶����λ����
        /// </summary>
        public const float EarthRadius = 6371000f; // ����뾶����λ����
        /// <summary>
        /// ����γ������ת��ΪWebī����ͶӰ����
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static Vector2d LonLatToWebMercator(double lon, double lat)
        {
            // ����ת��
            double x = lon * OriginShift / 180.0;

            // γ��ת��
            //double latRad = lat * Math.PI / 180.0;
            double y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360.0)) / (Math.PI / 180.0);
            y = y * OriginShift / 180.0;

            return new Vector2d(x, y);
        }

        /// <summary>
        /// ��Webī����ͶӰ����ת��Ϊ��γ������,���ű���Ϊ1
        /// ע�⣺��γ�������Webī����ͶӰ���������ϵ��һ����Webī����ͶӰ��������ĵ��ھ��ߵ������������ȷ�ΧΪ[-20037508.34, 20037508.34]��γ�ȷ�ΧΪ[-180, 85.0511287798066]
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="centerPoint">���ĵ�</param>
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
            // ����γ�ȴӶ���ת��Ϊ����
            //floatת��Ϊdouble�����⾫�ȶ�ʧ

            double latRad = (Mathf.Deg2Rad * latitude);
            double lonRad = (Mathf.Deg2Rad * longitude);

            // ʹ�����������x, y, zֵ
            double x = EarthRadius * Math.Cos(latRad) * Math.Cos(lonRad);
            double y = EarthRadius * Math.Cos(latRad) * Math.Sin(lonRad);
            double z = EarthRadius * Math.Sin(latRad);

            return new Vector2((float)x, (float)z);
            //return new Vector3((float)x, 0, (float)z);
        }
    }
}