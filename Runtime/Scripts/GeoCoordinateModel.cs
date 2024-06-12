using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATGC.GEO
{
    /// <summary>
    /// ��������ģ��
    /// </summary>
    [Serializable]
    public class GeoCoordinateModel
    {
        /// <summary>
        /// γ��
        /// </summary>
        public double latitude = 34.05f;   // γ��
        /// <summary>
        /// ����
        /// </summary>
        public double longitude = -118.25f; // ����

        /// <summary>
        /// ī����ͶӰ����
        /// </summary>
        public Vector2d WebMercatorPos;
        public double calc_distance;
        //���㵥λ����
        public Vector2d calc_directionVector;
        public Vector2d calc_unitVector;
        public Vector2d calc_unityPos;

        public GeoCoordinateModel(double lat, double lon)
        {
            latitude = lat;
            longitude = lon;
            CalculateWebMercatorPos();
        }

        /// <summary>
        /// ����ī����ͶӰ����
        /// </summary>
        public void CalculateWebMercatorPos()
        {
            WebMercatorPos = GeoUtils.LonLatToWebMercator(longitude, latitude);
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <param name="centerPos"></param>
        /// <returns></returns>
        public Vector2d GetRelativePos(Vector2d centerPos)
        {
            return new Vector2d(WebMercatorPos.x - centerPos.x, WebMercatorPos.y - centerPos.y);
        }

        /// <summary>
        /// CalculateInfo - ������Ϣ
        /// </summary>
        public void CalculateInfo(Vector2d centerWebMercatorPos)
        {
            //���������ĵ�ľ���
            calc_distance = Vector2d.Distance(WebMercatorPos, centerWebMercatorPos);
            //���㵥λ����
            calc_directionVector = WebMercatorPos - centerWebMercatorPos;
            calc_unitVector = calc_directionVector.normalized;

        }

        /// <summary>
        /// �������ź��unity����
        /// </summary>
        /// <param name="scale"></param>
        public Vector2 CalculateScaledUnityPos(Vector2 centerUnityPos, float scale)
        {
            //�����ӳ������µ��λ��
            if (scale == 0) return centerUnityPos;
            Vector2d centerWebMercatorPos = new Vector2d(centerUnityPos.x, centerUnityPos.y);
            calc_unityPos = centerWebMercatorPos + (calc_unitVector * calc_distance / scale);
            return new Vector2((float)calc_unityPos.x, (float)calc_unityPos.y);
        }
    }
}