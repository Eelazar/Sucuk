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


public class PROCESSOR_NUMBER : global::System.IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal PROCESSOR_NUMBER(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(PROCESSOR_NUMBER obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~PROCESSOR_NUMBER() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_PROCESSOR_NUMBER(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public ushort Group { set { AkSoundEnginePINVOKE.CSharp_PROCESSOR_NUMBER_Group_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_PROCESSOR_NUMBER_Group_get(swigCPtr); } 
  }

  public byte Number { set { AkSoundEnginePINVOKE.CSharp_PROCESSOR_NUMBER_Number_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_PROCESSOR_NUMBER_Number_get(swigCPtr); } 
  }

  public byte Reserved { set { AkSoundEnginePINVOKE.CSharp_PROCESSOR_NUMBER_Reserved_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_PROCESSOR_NUMBER_Reserved_get(swigCPtr); } 
  }

  public PROCESSOR_NUMBER() : this(AkSoundEnginePINVOKE.CSharp_new_PROCESSOR_NUMBER(), true) {
  }

}
#endif // #if UNITY_WSA && ! UNITY_EDITOR