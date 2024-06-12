using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATGC.GEO
{
    /// <summary>
    /// ������
    /// </summary>
    public class LocaterObj : MonoBehaviour
    {
        public string pixelInput = "114.117645,22.531821"; //�������������
        /// <summary>
        /// γ��
        /// </summary>
        private double latitude = 34.05f;   // γ��
        /// <summary>
        /// ����
        /// </summary>
        private double longitude = -118.25f; // ����

        /// <summary>
        /// SGGeoCoordinateModel ����
        /// </summary>
        [SerializeField]
        public GeoCoordinateModel m_GeoCoordinateModel;

        /// <summary>
        /// ��ʼ��GeoCoordinateModel
        /// </summary>
        public void InitGeoCoordinateModel()
        {
            if (!string.IsNullOrEmpty(pixelInput))
            {
                //�ֽ����γ��
                string[] strs = pixelInput.Split(',');
                if (strs.Length == 2)
                {
                    double.TryParse(strs[0], out longitude);
                    double.TryParse(strs[1], out latitude);
                }
            }
            m_GeoCoordinateModel = new GeoCoordinateModel(latitude, longitude);
        }


    }
}