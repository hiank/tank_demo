// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: master.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace MasterPb {

  /// <summary>Holder for reflection information generated from master.proto</summary>
  public static partial class MasterReflection {

    #region Descriptor
    /// <summary>File descriptor for master.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MasterReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxtYXN0ZXIucHJvdG8SCW1hc3Rlcl9wYiIPCg1HX01hc3Rlcl9Sb2xlIlEK",
            "BFJvbGUSCwoDdWlkGAEgASgEEg8KB21vZGVsSWQYAiABKAUSDwoHbW9kZWxM",
            "dhgDIAEoBRILCgNjdXAYBCABKAUSDQoFdW5hbWUYBSABKAlCKFomZ2l0aHVi",
            "LmNvbS9oaWFuay90aGlua2VuZC9tYXN0ZXIvcHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::MasterPb.G_Master_Role), global::MasterPb.G_Master_Role.Parser, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::MasterPb.Role), global::MasterPb.Role.Parser, new[]{ "Uid", "ModelId", "ModelLv", "Cup", "Uname" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class G_Master_Role : pb::IMessage<G_Master_Role> {
    private static readonly pb::MessageParser<G_Master_Role> _parser = new pb::MessageParser<G_Master_Role>(() => new G_Master_Role());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<G_Master_Role> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MasterPb.MasterReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public G_Master_Role() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public G_Master_Role(G_Master_Role other) : this() {
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public G_Master_Role Clone() {
      return new G_Master_Role(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as G_Master_Role);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(G_Master_Role other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(G_Master_Role other) {
      if (other == null) {
        return;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
        }
      }
    }

  }

  public sealed partial class Role : pb::IMessage<Role> {
    private static readonly pb::MessageParser<Role> _parser = new pb::MessageParser<Role>(() => new Role());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Role> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::MasterPb.MasterReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Role() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Role(Role other) : this() {
      uid_ = other.uid_;
      modelId_ = other.modelId_;
      modelLv_ = other.modelLv_;
      cup_ = other.cup_;
      uname_ = other.uname_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Role Clone() {
      return new Role(this);
    }

    /// <summary>Field number for the "uid" field.</summary>
    public const int UidFieldNumber = 1;
    private ulong uid_;
    /// <summary>
    ///NOTE: 玩家id，用64位，避免注册玩家过多
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    /// <summary>Field number for the "modelId" field.</summary>
    public const int ModelIdFieldNumber = 2;
    private int modelId_;
    /// <summary>
    ///NOTE: 模型id
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ModelId {
      get { return modelId_; }
      set {
        modelId_ = value;
      }
    }

    /// <summary>Field number for the "modelLv" field.</summary>
    public const int ModelLvFieldNumber = 3;
    private int modelLv_;
    /// <summary>
    ///NOTE: 模型等级
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ModelLv {
      get { return modelLv_; }
      set {
        modelLv_ = value;
      }
    }

    /// <summary>Field number for the "cup" field.</summary>
    public const int CupFieldNumber = 4;
    private int cup_;
    /// <summary>
    ///NOTE: 奖杯数，用于匹配
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Cup {
      get { return cup_; }
      set {
        cup_ = value;
      }
    }

    /// <summary>Field number for the "uname" field.</summary>
    public const int UnameFieldNumber = 5;
    private string uname_ = "";
    /// <summary>
    ///NOTE: 玩家名字
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Uname {
      get { return uname_; }
      set {
        uname_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Role);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Role other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Uid != other.Uid) return false;
      if (ModelId != other.ModelId) return false;
      if (ModelLv != other.ModelLv) return false;
      if (Cup != other.Cup) return false;
      if (Uname != other.Uname) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Uid != 0UL) hash ^= Uid.GetHashCode();
      if (ModelId != 0) hash ^= ModelId.GetHashCode();
      if (ModelLv != 0) hash ^= ModelLv.GetHashCode();
      if (Cup != 0) hash ^= Cup.GetHashCode();
      if (Uname.Length != 0) hash ^= Uname.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(Uid);
      }
      if (ModelId != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(ModelId);
      }
      if (ModelLv != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(ModelLv);
      }
      if (Cup != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Cup);
      }
      if (Uname.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(Uname);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Uid != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uid);
      }
      if (ModelId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ModelId);
      }
      if (ModelLv != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ModelLv);
      }
      if (Cup != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Cup);
      }
      if (Uname.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Uname);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Role other) {
      if (other == null) {
        return;
      }
      if (other.Uid != 0UL) {
        Uid = other.Uid;
      }
      if (other.ModelId != 0) {
        ModelId = other.ModelId;
      }
      if (other.ModelLv != 0) {
        ModelLv = other.ModelLv;
      }
      if (other.Cup != 0) {
        Cup = other.Cup;
      }
      if (other.Uname.Length != 0) {
        Uname = other.Uname;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Uid = input.ReadUInt64();
            break;
          }
          case 16: {
            ModelId = input.ReadInt32();
            break;
          }
          case 24: {
            ModelLv = input.ReadInt32();
            break;
          }
          case 32: {
            Cup = input.ReadInt32();
            break;
          }
          case 42: {
            Uname = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code