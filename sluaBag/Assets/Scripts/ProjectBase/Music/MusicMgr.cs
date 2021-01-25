using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : BaseManager<MusicMgr>
{
    //背景唯一的音乐组件
    private AudioSource bkMusic = null;
    //音乐大小
    private float bkValue = 1;

    //音效依附对象
    private GameObject soundObj = null;
    //音效列表
    private List<AudioSource> soundList = new List<AudioSource>();
    //音效大小
    private float soundValue = 1;

    public MusicMgr() {
        MonoManager.GetInstance().AddUpdateListener(Update);
    
    }
    private void Update() {
        for (int i = soundList.Count - 1; i >= 0; --i) {
            if (!soundList[i].isPlaying) {   //是否在播放
                //soundList[i].Stop();
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        
        }
    }


    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBkMusic(string name) {

        if (bkMusic == null) {
            GameObject obj = new GameObject();
            obj.name = "BKMusic";
            bkMusic = obj.AddComponent<AudioSource>();
        }
        //异步加载背景音乐，加载完成后播放
        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/BK/" + name,(clip)=> {
            bkMusic.clip = clip;
            bkMusic.loop = true;
            bkMusic.volume = bkValue;
            bkMusic.Play();
        });
    }

    /// <summary>
    /// 改变音量大小
    /// </summary>
    /// <param name="v">传递的要变成的音量值</param>
    public void changeBKValue(float v) {
        bkValue = v;
        if (bkMusic == null) {
            return;
        }
        bkMusic.volume = bkValue;
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBKMusic() {
        if (bkMusic == null)
        {
            return;
        }
        bkMusic.Pause();

    }


    /// <summary>
    /// 停止播放背景音乐
    /// </summary>
    public void StopBkMusic() {
        if (bkMusic == null) {
            return;
        }
        bkMusic.Stop();

    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callback = null) {

        if (soundObj == null) { 
            soundObj = new GameObject();
            soundObj.name = "Sound";
        
        }
        
        //当音效资源异步加载结束后，再添加一个音效
        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/Sound/" + name, (clip) => {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = bkValue;
            source.Play();
            if (callback != null) {
                callback(source);
            }
            soundList.Add(source);
        });
        


    }
    /// <summary>
    /// 停止播放音效
    /// </summary>
    public void StopSound(AudioSource source) {
        if (soundList.Contains(source)) {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        
        }
    
    }


    /// <summary>
    /// 把所有音效的声音大小都改变
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(float value) {
        soundValue = value;
        for (int i = 0; i < soundList.Count; i++) {
            soundList[i].volume = value;
        }
    }

}
