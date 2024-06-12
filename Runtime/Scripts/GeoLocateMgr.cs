using ATGC.GEO;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ATGC.GEO
{
    public class GeoLocateMgr : MonoSingletonNoDontDestroy<GeoLocateMgr>
    {

        /// <summary>
        /// ��׼����
        /// </summary>
        public LocaterObj CenterLocaterObj;
        /// <summary>
        /// ���ű���
        /// </summary>
        public float Scale = 1;

        /// <summary>
        /// ����LocaterObj�ļ���
        /// </summary>
        public List<LocaterObj> m_LocaterObjs = new List<LocaterObj>();

        private void Start()
        {
            //m_LocaterObjs ��ʼ��
            m_LocaterObjs = FindObjectsByType<LocaterObj>(FindObjectsSortMode.InstanceID).ToList();
            UpdateLocaterObjsByBase();
        }

        /// <summary>
        /// ͨ��BaseLocaterObj������������LocaterObj��λ��
        /// </summary>
        public void UpdateLocaterObjsByBase()
        {
            if (CenterLocaterObj == null)
            {
                return;
            }
            CenterLocaterObj.InitGeoCoordinateModel();
            // ��������LocaterObj, ���е�obj���������ȥBaseLocaterObj��SphereCoords
            foreach (LocaterObj obj in m_LocaterObjs)
            {
                if (obj == CenterLocaterObj)
                {
                    continue;
                }
                //ͨ��centerLocaterObj����obj������
                obj.InitGeoCoordinateModel();
                obj.m_GeoCoordinateModel.CalculateInfo(CenterLocaterObj.m_GeoCoordinateModel.WebMercatorPos);
                //�������ź��unity����
                Vector2 centerUnityPos = new Vector2(CenterLocaterObj.transform.position.x, CenterLocaterObj.transform.position.z);
                var scaledUnityPos = obj.m_GeoCoordinateModel.CalculateScaledUnityPos(centerUnityPos, Scale);
                obj.transform.position = new Vector3(scaledUnityPos.x, 0, scaledUnityPos.y);
            }
        }


    }
}