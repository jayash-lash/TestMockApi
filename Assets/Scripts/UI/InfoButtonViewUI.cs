using System;
using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InfoButtonViewUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _id;
        [SerializeField] private TextMeshProUGUI _name;

        public void SetButtonViewData(ButtonData buttonData)
        {
            ConvertColorFromHsLtoHSV(buttonData);
            _image.color = new Color(buttonData.Color[0], buttonData.Color[1], buttonData.Color[2]);
            _id.text = buttonData.Id.ToString();
            _name.text = buttonData.FirstName;
        
        }
        
        private void ConvertColorFromHsLtoHSV(ButtonData buttonData)
        {
            var color = buttonData.Color;
            var h = color[0];
            var s = color[1];
            var l = color[2];

            var v = l + s * Math.Min(l, 1 - l);
            var sv = (v != 0) ? 2 * (1 - l / v) : 0;

            buttonData.Color = new[] { h, sv, v };
        }

        public void PlayButtonAnimation(ButtonData buttonData)
        {
            if (buttonData.AnimationType)
            {
                transform.localScale = Vector3.zero;

                transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            }
        }
    }
}