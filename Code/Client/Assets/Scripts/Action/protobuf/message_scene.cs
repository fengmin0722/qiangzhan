//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: message_scene.hxx
namespace Message
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"role_stage")]
  public partial class role_stage : global::ProtoBuf.IExtensible
  {
    public role_stage() {}
    
    private uint _stage_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stage_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint stage_id
    {
      get { return _stage_id; }
      set { _stage_id = value; }
    }
    private uint _max_grade;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"max_grade", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint max_grade
    {
      get { return _max_grade; }
      set { _max_grade = value; }
    }

    private uint _kill_rate = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"kill_rate", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint kill_rate
    {
      get { return _kill_rate; }
      set { _kill_rate = value; }
    }

    private uint _max_combo = default(uint);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"max_combo", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint max_combo
    {
      get { return _max_combo; }
      set { _max_combo = value; }
    }

    private uint _passtime_record = default(uint);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"passtime_record", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint passtime_record
    {
      get { return _passtime_record; }
      set { _passtime_record = value; }
    }

    private uint _pass_times = default(uint);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"pass_times", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint pass_times
    {
      get { return _pass_times; }
      set { _pass_times = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"role_stage_daily")]
  public partial class role_stage_daily : global::ProtoBuf.IExtensible
  {
    public role_stage_daily() {}
    
    private uint _stage_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stage_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint stage_id
    {
      get { return _stage_id; }
      set { _stage_id = value; }
    }
    private uint _daily_times;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"daily_times", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint daily_times
    {
      get { return _daily_times; }
      set { _daily_times = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"role_dropaward")]
  public partial class role_dropaward : global::ProtoBuf.IExtensible
  {
    public role_dropaward() {}
    
    private int _drop_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"drop_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int drop_id
    {
      get { return _drop_id; }
      set { _drop_id = value; }
    }
    private int _drop_num;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"drop_num", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int drop_num
    {
      get { return _drop_num; }
      set { _drop_num = value; }
    }
    private int _drop_id_type;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"drop_id_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int drop_id_type
    {
      get { return _drop_id_type; }
      set { _drop_id_type = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"role_stageaward")]
  public partial class role_stageaward : global::ProtoBuf.IExtensible
  {
    public role_stageaward() {}
    
    private uint _stage_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stage_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint stage_id
    {
      get { return _stage_id; }
      set { _stage_id = value; }
    }

    private uint _drop_gold = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"drop_gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint drop_gold
    {
      get { return _drop_gold; }
      set { _drop_gold = value; }
    }
    private readonly global::System.Collections.Generic.List<role_dropaward> _first_award_id = new global::System.Collections.Generic.List<role_dropaward>();
    [global::ProtoBuf.ProtoMember(3, Name=@"first_award_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<role_dropaward> first_award_id
    {
      get { return _first_award_id; }
    }
  
    private readonly global::System.Collections.Generic.List<role_dropaward> _dropbox_id = new global::System.Collections.Generic.List<role_dropaward>();
    [global::ProtoBuf.ProtoMember(4, Name=@"dropbox_id", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<role_dropaward> dropbox_id
    {
      get { return _dropbox_id; }
    }
  
    private readonly global::System.Collections.Generic.List<role_dropaward> _normal_random = new global::System.Collections.Generic.List<role_dropaward>();
    [global::ProtoBuf.ProtoMember(5, Name=@"normal_random", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<role_dropaward> normal_random
    {
      get { return _normal_random; }
    }
  
    private readonly global::System.Collections.Generic.List<role_dropaward> _extra_random = new global::System.Collections.Generic.List<role_dropaward>();
    [global::ProtoBuf.ProtoMember(6, Name=@"extra_random", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<role_dropaward> extra_random
    {
      get { return _extra_random; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"role_stageinfo")]
  public partial class role_stageinfo : global::ProtoBuf.IExtensible
  {
    public role_stageinfo() {}
    
    private readonly global::System.Collections.Generic.List<role_stage> _stages = new global::System.Collections.Generic.List<role_stage>();
    [global::ProtoBuf.ProtoMember(1, Name=@"stages", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<role_stage> stages
    {
      get { return _stages; }
    }
  
    private readonly global::System.Collections.Generic.List<role_stage_daily> _stages_daily = new global::System.Collections.Generic.List<role_stage_daily>();
    [global::ProtoBuf.ProtoMember(2, Name=@"stages_daily", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<role_stage_daily> stages_daily
    {
      get { return _stages_daily; }
    }
  

    private role_stageaward _awards = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"awards", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public role_stageaward awards
    {
      get { return _awards; }
      set { _awards = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"request_msg_enterstage")]
  public partial class request_msg_enterstage : global::ProtoBuf.IExtensible
  {
    public request_msg_enterstage() {}
    
    private uint _scene_type;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"scene_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint scene_type
    {
      get { return _scene_type; }
      set { _scene_type = value; }
    }
    private uint _scene_id;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"scene_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint scene_id
    {
      get { return _scene_id; }
      set { _scene_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"respond_msg_enterstage")]
  public partial class respond_msg_enterstage : global::ProtoBuf.IExtensible
  {
    public respond_msg_enterstage() {}
    
    private int _result;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int result
    {
      get { return _result; }
      set { _result = value; }
    }

    private uint _scene_type = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"scene_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint scene_type
    {
      get { return _scene_type; }
      set { _scene_type = value; }
    }

    private uint _scene_id = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"scene_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint scene_id
    {
      get { return _scene_id; }
      set { _scene_id = value; }
    }

    private role_stageaward _awards = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"awards", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public role_stageaward awards
    {
      get { return _awards; }
      set { _awards = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"request_msg_passstage")]
  public partial class request_msg_passstage : global::ProtoBuf.IExtensible
  {
    public request_msg_passstage() {}
    
    private role_stage _stage;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stage", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public role_stage stage
    {
      get { return _stage; }
      set { _stage = value; }
    }
    private uint _normal_award_count;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"normal_award_count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint normal_award_count
    {
      get { return _normal_award_count; }
      set { _normal_award_count = value; }
    }
    private uint _extra_award_count;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"extra_award_count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint extra_award_count
    {
      get { return _extra_award_count; }
      set { _extra_award_count = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"respond_msg_passstage")]
  public partial class respond_msg_passstage : global::ProtoBuf.IExtensible
  {
    public respond_msg_passstage() {}
    
    private int _result;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int result
    {
      get { return _result; }
      set { _result = value; }
    }

    private role_stage _stage = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"stage", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public role_stage stage
    {
      get { return _stage; }
      set { _stage = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"request_msg_relive")]
  public partial class request_msg_relive : global::ProtoBuf.IExtensible
  {
    public request_msg_relive() {}
    
    private uint _scene_type;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"scene_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint scene_type
    {
      get { return _scene_type; }
      set { _scene_type = value; }
    }
    private uint _scene_id;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"scene_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint scene_id
    {
      get { return _scene_id; }
      set { _scene_id = value; }
    }
    private uint _relive_type;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"relive_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint relive_type
    {
      get { return _relive_type; }
      set { _relive_type = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"respond_msg_relive")]
  public partial class respond_msg_relive : global::ProtoBuf.IExtensible
  {
    public respond_msg_relive() {}
    
    private int _result;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int result
    {
      get { return _result; }
      set { _result = value; }
    }

    private uint _relive_type = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"relive_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint relive_type
    {
      get { return _relive_type; }
      set { _relive_type = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"request_msg_unlockstage")]
  public partial class request_msg_unlockstage : global::ProtoBuf.IExtensible
  {
    public request_msg_unlockstage() {}
    
    private uint _stage_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stage_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint stage_id
    {
      get { return _stage_id; }
      set { _stage_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"respond_msg_unlockstage")]
  public partial class respond_msg_unlockstage : global::ProtoBuf.IExtensible
  {
    public respond_msg_unlockstage() {}
    
    private uint _stage_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stage_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint stage_id
    {
      get { return _stage_id; }
      set { _stage_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"request_msg_sp_increase")]
  public partial class request_msg_sp_increase : global::ProtoBuf.IExtensible
  {
    public request_msg_sp_increase() {}
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"respond_msg_sp_increase")]
  public partial class respond_msg_sp_increase : global::ProtoBuf.IExtensible
  {
    public respond_msg_sp_increase() {}
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"request_msg_zone_reward")]
  public partial class request_msg_zone_reward : global::ProtoBuf.IExtensible
  {
    public request_msg_zone_reward() {}
    
    private int _zone_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"zone_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int zone_id
    {
      get { return _zone_id; }
      set { _zone_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"respond_msg_zone_reward")]
  public partial class respond_msg_zone_reward : global::ProtoBuf.IExtensible
  {
    public respond_msg_zone_reward() {}
    
    private int _errorcode;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"errorcode", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int errorcode
    {
      get { return _errorcode; }
      set { _errorcode = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}