#if UNITY_WSA && ! UNITY_EDITOR
//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AkPlatformInitSettings : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkPlatformInitSettings(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkPlatformInitSettings obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkPlatformInitSettings() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkPlatformInitSettings(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public AkThreadProperties threadLEngine { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadLEngine_set(swigCPtr, AkThreadProperties.getCPtr(value)); } 
    get {
      global::System.IntPtr cPtr = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadLEngine_get(swigCPtr);
      AkThreadProperties ret = (cPtr == global::System.IntPtr.Zero) ? null : new AkThreadProperties(cPtr, false);
      return ret;
    } 
  }

  public AkThreadProperties threadBankManager { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadBankManager_set(swigCPtr, AkThreadProperties.getCPtr(value)); } 
    get {
      global::System.IntPtr cPtr = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadBankManager_get(swigCPtr);
      AkThreadProperties ret = (cPtr == global::System.IntPtr.Zero) ? null : new AkThreadProperties(cPtr, false);
      return ret;
    } 
  }

  public AkThreadProperties threadMonitor { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadMonitor_set(swigCPtr, AkThreadProperties.getCPtr(value)); } 
    get {
      global::System.IntPtr cPtr = AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_threadMonitor_get(swigCPtr);
      AkThreadProperties ret = (cPtr == global::System.IntPtr.Zero) ? null : new AkThreadProperties(cPtr, false);
      return ret;
    } 
  }

  public uint uLEngineDefaultPoolSize { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uLEngineDefaultPoolSize_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uLEngineDefaultPoolSize_get(swigCPtr); } 
  }

  public float fLEngineDefaultPoolRatioThreshold { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_fLEngineDefaultPoolRatioThreshold_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_fLEngineDefaultPoolRatioThreshold_get(swigCPtr); } 
  }

  public ushort uNumRefillsInVoice { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uNumRefillsInVoice_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uNumRefillsInVoice_get(swigCPtr); } 
  }

  public uint uSampleRate { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uSampleRate_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_uSampleRate_get(swigCPtr); } 
  }

  public AkAudioAPI eAudioAPI { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_eAudioAPI_set(swigCPtr, (int)value); }  get { return (AkAudioAPI)AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_eAudioAPI_get(swigCPtr); } 
  }

  public bool bGlobalFocus { set { AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_bGlobalFocus_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkPlatformInitSettings_bGlobalFocus_get(swigCPtr); } 
  }

  public AkPlatformInitSettings() : this(AkSoundEnginePINVOKE.CSharp_new_AkPlatformInitSettings(), true) {
  }

}
#endif // #if UNITY_WSA && ! UNITY_EDITOR