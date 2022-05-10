using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrypts.AdsNew
{
    public interface IAdsProvider
    {
        /// <summary>
        /// Инициализировать провайдер
        /// </summary>
        /// <param name="isChildDirected">детский контент</param>
        void Init(bool isChildDirected = false);

        /// <summary>
        /// Деструктор
        /// </summary>
        void Deinit();

        /// <summary>
        /// Загрузить Rewarded видео видео
        /// </summary>
        void LoadRewardedVideo(GameObject gameObject);

        /// <summary>
        /// Готово ли Rewarded видео
        /// </summary>
        /// <returns>true - если готово к показу, в противном случае false</returns>
        bool IsReadyRewardedVideo();

        /// <summary>
        /// Показать Rewarded видео
        /// </summary>
        void ShowRewardedVideo();

        /// <summary>
        /// Завершение просмотра Reward видео
        /// </summary>
        Action RewardedVideoFinished { get; set; }

        /// <summary>
        /// Пропуск Reward видео
        /// </summary>
        Action RewardedVideoSkipped { get; set; }
    }
}
