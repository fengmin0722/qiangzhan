using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// 定义BattleUnit, Player, Npc等的技能.
/// </summary>
public class BattleUnitSkill
{
	public BattleUnitSkill(uint resID)
	{
		skillRes = DataManager.SkillCommonTable[resID] as SkillCommonTableItem;
		if (skillRes == null)
			SkillUtilities.ResourceNotFound("skillcommon", resID);
	}

	public SkillCommonTableItem skillRes;

	// 技能上次使用的时间, 如果为MaxValue, 表示技能尚未使用过.
    public ulong lastUseTime = ulong.MaxValue;
	public bool enabled = true;
	
	/// <summary>
	/// 获取CD时间的毫秒数.
	/// </summary>
	public uint CdMilliseconds {
		get {
			uint cdNow = 0;
            if (lastUseTime != ulong.MaxValue)
			{
                ulong now = TimeUtilities.GetNow();
				cdNow = (uint)(now - lastUseTime);
				cdNow = (cdNow >= skillRes.myCd) ? 0 : (skillRes.myCd - cdNow);
			}

			return cdNow;
		}
	}

    /// <summary>
    /// 重置cd时间
    /// </summary>
    public void ResetCd()
    {
        lastUseTime = ulong.MaxValue;
    }

	/// <summary>
	/// 是否为普通攻击.
	/// </summary>
	public bool IsRegularAttack {
		get {
			return skillRes != null && skillRes.isRegularAttack;
		}
	}
}

/// <summary>
/// BattleUnit的技能容器.
/// 包含BattleUnit的所有技能.
/// </summary>
public class BattleUnitSkillContainer
{
    private Hashtable mSkillContainer = new Hashtable();

	public BattleUnitSkillContainer()
	{
        mSkillContainer = new Hashtable();
	}

	public BattleUnitSkill GetSkill(int resID)
	{
        if (!mSkillContainer.ContainsKey(resID))
        {
            BattleUnitSkill skill = new BattleUnitSkill((uint)resID);
			if (skill.skillRes == null)
				return null;

            mSkillContainer.Add(resID, skill);
            return skill;
        }

        return mSkillContainer[resID] as BattleUnitSkill;
	}

    public void ResetAllSkillCD()
    {
		foreach (BattleUnitSkill skill in mSkillContainer.Values)
			skill.ResetCd();
    }

	public void ResetSkillCD(int resID)
	{
		BattleUnitSkill skill = mSkillContainer[resID] as BattleUnitSkill;
		if (skill != null)
			skill.ResetCd();
	}
}

/// <summary>
/// 随机事件的触发条件类型.
/// </summary>
public enum RandEventTriggerType : uint
{ 
	OnAtkOthers,			// 攻击到别人时触发.
	OnBeAttacked,			// 被攻击时触发.
	OnKillOthers,			// 当击杀别人时触发.
	OnBeKilled,				// 当拥有者死亡时触发.
	OnStartRegularSkill,	// 开始使用普通攻击技能时触发.
	OnFatalStrike,			// 受到致命一击时触发.
	OnCriticalStrikeOthers,	// 暴击他人时.
	OnSkillFinished,		// 使用过技能之后触发.
	Count,
	Invalid = uint.MaxValue
}

/// <summary>
/// 技能效果脚本定义.
/// 顺序与SkillScriptFactory::typeContainer中的一致.
/// </summary>
public enum SkillScriptDef : uint
{
	AphoticShield,
	Ballistics,
	Vampirism,
	DeathCreation,
	CorpseExplosion,
	SkillEffectPlacer,
	Reincarnation,
	CounterSkillEffectGenerator,
	CriticalStrikeSkillEffectGenerator,
	Overload,
	Refraction,
	Count,
	Invalid = uint.MaxValue
}

/// <summary>
/// 随机事件的参数.
/// 该参数为调用randevent的入/出参数.
/// </summary>
public abstract class RandEventArg
{ }

/// <summary>
/// 攻击到他人时的randevent的参数.
/// </summary>
public class AtkOthersEventArg : RandEventArg
{
	/// <summary>
	/// 被攻击的目标.
	/// </summary>
	public BattleUnit Target { get; set; }

	/// <summary>
	/// 对对方造成的伤害.
	/// </summary>
	public int FireDamage { get; set; }

	public AtkOthersEventArg(BattleUnit target, int damage)
	{
		Target = target; 
		FireDamage = damage;
	}
}

/// <summary>
/// 被攻击时的randevent的参数.
/// </summary>
public class BeAttackedEventArg :RandEventArg
{
	/// <summary>
	/// 遭受的伤害. 该值可以被改变, 从而实现减/免伤.
	/// </summary>
	public int PositiveDamage { get; set; }
	public BeAttackedEventArg(int damage) { PositiveDamage = damage; }
}

/// <summary>
/// 击杀他人时的randevent的参数.
/// </summary>
public class KillOthersEventArg : RandEventArg
{
}

/// <summary>
/// 被击杀时的randevent的参数.
/// </summary>
public class OnBeKilledEventArg : RandEventArg
{
	/// <summary>
	/// 凶手释放击杀技能时的属性.
	/// </summary>
	public AttackerAttr KillerAttr { get; set; }
	public OnBeKilledEventArg(AttackerAttr killerAttr)
	{
		KillerAttr = killerAttr;
	}
}

/// <summary>
/// 开始使用普通攻击时的randevent参数.
/// </summary>
public class StartRegularSkillEventArg : RandEventArg
{
	/// <summary>
	/// <para>被发起的普通技能的ID.</para>
	/// 事件发生前后, 该技能ID可能发生改变, 从而改变弹道.
	/// </summary>
	public uint SkillResID { get; set; }
	public StartRegularSkillEventArg(uint skillResID)
	{
		SkillResID = skillResID;
	}
}

/// <summary>
/// 受到致命一击时的randevent参数.
/// </summary>
public class FatalStrikeEventArg : RandEventArg
{
	/// <summary>
	/// 是否格挡此次致命伤害.
	/// </summary>
	public bool BlockThisDamage { get; set; }
	public FatalStrikeEventArg()
	{
		BlockThisDamage = false;
	}
}

/// <summary>
/// 暴击对方时的随机事件.
/// </summary>
public class OnCriticalStrikeOthersEventArg : RandEventArg
{
}

/// <summary>
/// 技能正常结束时的随机事件.
/// </summary>
public class OnSkillFinishedEventArg : RandEventArg
{
	public SkillCommonTableItem SkillRes { get; private set; }
	public OnSkillFinishedEventArg(SkillCommonTableItem skillResource)
	{
		SkillRes = skillResource;
	}
}

/// <summary>
/// impact的伤害类型.
/// </summary>
public enum ImpactDamageType : uint
{ 
	None = 0,					// 无.
	Frost,						// 冰.
	Fire,						// 火.
	Lightning,					// 电.
	Poison,						// 毒.
	Count,
	Invalid = uint.MaxValue,	// 无效.
}

/// <summary>
/// 每种被击材质特效和被击声音的CD时间容器.
/// 控制指定伤害类型的伤害特效的播放频率.
/// </summary>
public class HitMaterialEffectCdContainer
{
	uint[] cdContainer = new uint[(int)ImpactDamageType.Count];
	static readonly uint kHitMaterialCd = GameConfig.HitMaterialCd;

	/// <summary>
	/// 更新被击材质特效播放的CD时间.
	/// </summary>
	/// <param name="elapsed"></param>
	public void Update(uint elapsed) {
		for (int i = cdContainer.Length - 1; i >= 0; --i) {
			if (cdContainer[i] >= elapsed)
				cdContainer[i] -= elapsed;
			else
				cdContainer[i] = 0;
		}
	}

	/// <summary>
	/// 返回指定类型的被击材质特效是否在CD时间内.
	/// </summary>
	public bool IsCoolingDown(ImpactDamageType damageType) {
		return cdContainer[(int)damageType] != 0;
	}

	public void StartCd(ImpactDamageType damageType)
	{
		cdContainer[(int)damageType] = kHitMaterialCd;
	}
}

public class BattleUnitRandEvent
{
	public BattleUnitRandEvent(SkillBuffTableItem buffRes, SkillRandEventTableItem resource)
	{
		fromBuffRes = buffRes;
		cdMilliseconds = 0;

		randEventResource = resource;
	}

	/// <summary>
	/// 加载该随机事件的触发脚本.
	/// </summary>
	/// <param name="owner"></param>
	public void LoadScript(BattleUnit owner, /*const*/ref AttackerAttr createrAttr)
	{
		if (randEventResource.triggerScript != SkillScriptDef.Invalid)
		{
			mScript = SkillScriptFactory.Instance.Allocate(randEventResource.triggerScript, owner);

			do
			{
				if (!checkScript(owner, mScript))
					break;

				SkillScriptStartArgument argument = new SkillScriptStartArgument() {
					buffCreaterAttr = createrAttr,
					buffRes = fromBuffRes,
					argument_0 = randEventResource.triggerArgument_0,
					argument_1 = randEventResource.triggerArgument_1,
					argument_2 = randEventResource.triggerArgument_2 
				};

				if (!mScript.StartScript(argument))
					break;

				return;

			} while (false);

			ErrorHandler.Parse(ErrorCode.ConfigError, "加载randevent[" + randEventResource.resID + "]附带的脚本失败");
			mScript = null;
		}
	}

	public void StopScript(SkillEffectStopReason stopReason)
	{
		if (mScript != null)
			mScript.StopScript(stopReason);
	}

	bool checkScript(BattleUnit owner, SkillScript script)
	{
		if (script.ScriptTriggerType != randEventResource.triggerType)
		{
			ErrorHandler.Parse(ErrorCode.ConfigError,
				   string.Format("随机事件[{0}]的触发类型({1})和脚本\"{2}\"的触发类型({3})不一致",
				   randEventResource.resID,
				   (uint)randEventResource.triggerType,
					script.GetType().ToString(),
				   (uint)script.ScriptTriggerType)
			   );
			return false;
		}

		return true;
	}

	// 来自哪个buff.
	public SkillBuffTableItem fromBuffRes;

	// 随机事件触发的CD时间(ms).
	public uint cdMilliseconds;

	// 资源.
	public SkillRandEventTableItem randEventResource;

	// 触发脚本.
	public SkillScript mScript = null;

	#region Comparison
	public override bool Equals(object obj)
	{
		return (obj is BattleUnitRandEvent) && Equals(obj as BattleUnitRandEvent);
	}

	public bool Equals(BattleUnitRandEvent other)
	{
		return other != null && other.fromBuffRes.resID == fromBuffRes.resID;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
	#endregion Comparison
}

public sealed class BattleUnitRandEventContainer
{
	// 随机事件按照类型, 放到不同的容器中.
	public List<BattleUnitRandEvent>[] mContainer = null;

	public BattleUnitRandEventContainer()
	{
        mContainer = new List<BattleUnitRandEvent>[(int)RandEventTriggerType.Count];
		for (int i = 0; i < mContainer.Length; ++i)
            mContainer[i] = new List<BattleUnitRandEvent>();
	}

	/// <summary>
	///获取指定类型的随机事件的容器.
	/// </summary>
    public List<BattleUnitRandEvent> this[RandEventTriggerType type]
	{
		get { return mContainer[(int)type]; }
	}

	public void Add(BattleUnit owner, SkillBuffTableItem buffRes, SkillRandEventTableItem randEventResource, /*const*/ref AttackerAttr buffCreaterAttr)
	{
		BattleUnitRandEvent item = new BattleUnitRandEvent(buffRes, randEventResource);
		item.LoadScript(owner, ref buffCreaterAttr);
		mContainer[(int)randEventResource.triggerType].Add(item);
	}

	public void Remove(SkillBuffTableItem buffRes, SkillEffectStopReason stopReason)
	{
		SkillRandEventTableItem randEventRes = DataManager.RandEventTable[buffRes.randEvent] as SkillRandEventTableItem;

        if( randEventRes == null )
        {
            return;
        }

        List<BattleUnitRandEvent> typedContainer = mContainer[(int)randEventRes.triggerType];

        BattleUnitRandEvent node = typedContainer.Find( x => (x.fromBuffRes.resID == buffRes.resID && x.randEventResource.resID == randEventRes.resID));  

		if (node != null)
		{
			node.StopScript(stopReason);
			typedContainer.Remove(node);
		}
	}

	public void Update(uint elapsed)
	{
        for (int i = 0; i < mContainer.Length; ++i )
        {
            List<BattleUnitRandEvent> list = mContainer[i];
            for (int j = 0; j < list.Count; ++j )
            {
                BattleUnitRandEvent element = list[j];
                if (element.cdMilliseconds > elapsed)
                    element.cdMilliseconds -= elapsed;
                else
                    element.cdMilliseconds = 0;
            }
//                 foreach (BattleUnitRandEvent element in list)
//                 {
//                     
//                 }
        }
//             foreach (LinkedList<BattleUnitRandEvent> list in mContainer)
//             {
//                 foreach (BattleUnitRandEvent element in list)
//                 {
//                     if (element.cdMilliseconds > elapsed)
//                         element.cdMilliseconds -= elapsed;
//                     else
//                         element.cdMilliseconds = 0;
//                 }
//             }
	}
}

/// <summary>
/// 可计数的属性. 默认为0.
/// </summary>
public enum ActiveFlagsDef : uint
{
    IsDead,

	// StunFlags: 与SkillUtilities.IsStunFlag, 范围一致.
	DisableMovement,			// 不可移动.
	DisableAttack,				// 不可普通攻击.
	DisableSkillUse,			// 不可使用技能.
	DisableRotate,				// 不可转向.
	DisableChangeWeaponModel,	// 不可改变武器模型.
	DisableChangeWeapon,		// 不可换武器.
	//

	StunImmunity,		// 不可被控制(包括移动, 攻击, 使用技能, 转向).
	DamageImmunity,		// 不可伤血.

	MagneticEffect,		// 磁铁效果, 扩大目标吸收Pick的范围.
	StrokeEffect,		// 描边效果.
	
	// 如果为真, 那么, 单位不会被敌方锁定, 不能被攻击, 而且不会阻挡子弹.
	// 该属性与免疫有害技能效果不同, 如果一个单位只是免疫有害技能效果, 依然会被锁定和阻挡子弹, 
	// 依然会被敌方由AI控制的单位锁定, 追击.
	Inviolability,

	// 变身的计数. 
	// 如果发生变身, 那么立刻执行; 如果取消变身, 需要当该计数为0时才进行.
	Transformed,

	Count,
}

/// <summary>
/// 动态标记, 包含移动, 使用技能等的行为的允许与否.
/// </summary>
public sealed class ActiveFlagsContainer
{
	public uint[] mFlagsContainer = new uint[(uint)ActiveFlagsDef.Count];
	public uint this[ActiveFlagsDef index]
	{
		get
		{
			return mFlagsContainer[(uint)index];
		}
	}

	public void AddCount(ActiveFlagsDef index, bool increase)
	{
		if (increase)
			++mFlagsContainer[(uint)index];
		else {
			if (mFlagsContainer[(uint)index] == 0)
			{
				ErrorHandler.Parse(ErrorCode.LogicError, "failed to DECREASE active flags " + index + ", which is already zero");
				return;
			}
			--mFlagsContainer[(uint)index];
		}
	}
}

public class SkillMaxCountDefine
{
	public static readonly uint MAX_EQUIP_SKILL_NUM = 3;
	public static readonly uint BATTLEUNIT_SKILL_NUM = 16;
}

/// <summary>
/// 见"技能表格说明.xlsx/bullettypedetails"
/// </summary>
public enum BulletType : uint
{
	// 简单的, 直线水平飞行的子弹.
	BulletTypeBasic = 0,
	// 坦克炮, 沿斜线落地飞行.
	BulletTypeCannonball,
	// 手雷, 抛物线飞行.
	BulletTypeGrenade,
	// 垂直下落, 轰炸.
	BulletTypeBomb,
	// 追踪弹.
	BulletTypeMissile,
	// 火箭炮, 爆炸方式与BulletTypeBasic不同之外, 其他都相同.
	BulletTypeRocket,

	// 按照函数图像飞行.
	BulletTypeFlyAlongEquationGraph,
	// 沿圆弧.
	BulletTypeCircleArc = BulletTypeFlyAlongEquationGraph,
	// 沿正弦曲线.
	BulletTypeSineWave,
	//
	BulletTypeCount,
}

public enum CreationType : uint
{ 
	Npc,
	Trap,
	Invalid = uint.MaxValue
}

public enum SkillEffectType : uint
{ 
	Buff,
	Impact,
	Displacement,
	Spasticity,
	Count,
	Invalid = uint.MaxValue
}

public enum SkillDisplacementType : uint
{
	Rush,		// 突进, 向当前朝向.
	Beatback,	// 击退, 向发起点的反方向.
	Count,
}

public enum PathEquationDef : uint 
{
	CircleArc,
	Wave,
	Count,
	Invalid = uint.MaxValue
}

public struct BulletDistributionResultDef
{
	public Vector3 StartPosition;		// 子弹起点.
	public Vector3 TargetPosition;		// 子弹终点.
	public uint Delay;					// 子弹延迟创建时间.
}

public struct Pair<T1, T2>
{
	public Pair(T1 _1, T2 _2) {
		first = _1;
		second = _2;
	}
	public T1 first;
	public T2 second;
}

public enum ClientBehaviourDef : uint
{ 
	RoleAnimation,			// 角色动画.
	WeaponAnimation,		// 武器动画.
	Sound,					// 声音.
	ThreeDEffect2Role,		// 角色身上特效.
	ThreeDEffectInScene,	// 场景特效.
	ChangeWeaponFigure,		// 改变武器模型.
	Invalid = uint.MaxValue,
}

/// <summary>
/// 材质被击和死亡表现.
/// </summary>
public enum MaterialBehaviourDef : uint
{ 
	// 材质被击时.
	OnMaterialBeHit,
	// 材质死亡.
	OnMaterialDie,
}

// owner死亡时, buff效果结束.
// 这和buff的死亡清除不一样, 如果一个buff在Owner死亡时不清除, 那么它依然需要被终止
// 并回收, 只是在库中记录buff的ID.
public enum BuffRemoveCondition : uint
{ 
	None = 0,

	// 死亡时是否清除.
	RemoveOnDie = 1,

	// 离开场景是否清除.
	RemoveOnLeaveScene = 2,

	RemoveOnDieAndLeaveScene = RemoveOnDie | RemoveOnLeaveScene,

	Count
}

/// <summary>
/// skilleffect的拥有者状态发生变化时, 通知skilleffectmanager, 改变其中的effect状态.
/// </summary>
public enum SkillEffectOwnerEventDef : uint
{ 
	OwnerDie,
	OwnerLeaveScene,
	Invalid = uint.MaxValue,
}

/// <summary>
/// 技能效果经由什么原因被终止.
/// </summary>
public enum SkillEffectStopReason : uint
{ 
	// 到达时间而结束.
	Expired,
	// 被净化而提前结束(比如被其他效果驱散, 互斥等原因).
	Diffused,
	// 被回收, 指在目标死亡或者退出场景时, 技能效果被终止回收.
	Recycled,
	Invalid = uint.MaxValue
}

/// <summary>
/// 子弹分布类型.
/// </summary>
public enum BulletDistributionType : uint
{ 
	ByAngle,			// 按角度.
	ByRandomOffset,		// 按终点随机偏移.
	ByData,				// 按数据指定.
	Count,
	Invalid = uint.MaxValue
}

/// <summary>
/// 投掷物创建参数.
/// </summary>
public class ProjectileCreateParam
{
	public BattleUnit User;
	public Vector3 StartPosition;
	public Vector3 TargetPosition;

	public BulletDistributionType DistributionType;
	public string DistributionArgument;

	public BulletTableItem BulletRes;

	public uint SkillResID;
}

public struct DistributionComputeParam {
	// 分布参数(用'|'分割的字符串).
	public string distributionArgument;
	// 子弹起始位置.
	public Vector3 startPosition;
	// 子弹结束位置.
	public Vector3 targetPosition;
}

/// <summary>
/// 战斗时的血量变化.
/// </summary>
public class DamageInfo
{
	// 伤害值, 正值表示加血, 否则为减血.
	public int Value;
	// 伤害是否是暴击(在-血时有效).
	public bool Critical = false;
	// 伤害类型(在-血时有效).
	public ImpactDamageType DamageType = ImpactDamageType.Invalid;
}

/// <summary>
/// 循环播放的前端表现的ID.
/// </summary>
public class ClientBehaviourIdContainer
{
	public int roleAnimationHashCode = 0;
	public uint weaponAnimation = uint.MaxValue;
	public uint soundId = uint.MaxValue;
	/// <summary>
	/// skillclientbehaviour中仅有两个绑定到使用者的特效.
	/// 即使技能可以多次进入使用阶段, 这两个也足够使用.
	/// 因为只有循环的特效的运行时ID才会保存, 而循环的特效在技能整个使用阶段, 只播放一次.
	/// </summary>
	public uint threeDEffect2Role_0 = uint.MaxValue;
	public uint threeDEffect2Role_1 = uint.MaxValue;
	public uint threeDEffectInScene = uint.MaxValue;

	public void Clear()
	{
		roleAnimationHashCode = 0;
		weaponAnimation = soundId = threeDEffect2Role_0 = threeDEffect2Role_1 = threeDEffectInScene = uint.MaxValue;
	}
}

/// <summary>
/// 技能, 技能效果的发起者的属性.
/// </summary>
/// <remarks>
/// 该属性单独存储, 而不是在使用时, 通过攻击者再获取, 避免在技能使用到命中之间, 
/// 使用者属性的变化对效果的影响.
/// </remarks>
public struct AttackerAttr
{
	AttackerAttrImpl mImpl;

	/// <summary>
	/// 初始化该结构.
	/// </summary>
	/// <param name="attacker">技能效果等的发起者(如果为null, 视为系统)</param>
	/// <param name="skillCommonID">发起者使用的技能ID(非技能发起填入uint.MaxValue).</param>
	/// <param name="structMadeByRandEvent">该结构是否由randevent创造(检测并避免randevent循环产生效果).</param>
	/// <seealso cref="AttackerAttrImpl"/>
	public AttackerAttr(BattleUnit attacker, uint skillCommonID = uint.MaxValue, bool structMadeByRandEvent = false)
	{
		mImpl = new AttackerAttrImpl(attacker, skillCommonID, structMadeByRandEvent);
	}
	/// <summary>
	/// 在场景中, 根据攻击者的ID, 重新获取对应的对象.
	/// 如果返回值不为null, 则必须是一个有效的场景内对象.
	/// </summary>
	public BattleUnit CheckedAttackerObject()
	{
		BaseScene scn = SceneManager.Instance.GetCurScene();
		return (AttackerID != uint.MaxValue && scn != null)
			? scn.FindObject(AttackerID) as BattleUnit : null;
	}

	#region ReadOnlyProperties
	public LeagueDef AttackerLeague { get { return mImpl.mAttackerLeague; } }
	public uint AttackerID { get { return mImpl.mAttackerID; } }
	public bool StructMadeByRandEvent { get { return mImpl.mStructMadeByRandEvent; } }
	public uint SkillCommonID { get { return mImpl.mSkillCommonID; } }
	public string AttackerName { get { return mImpl.mAttackerName; } }
	public float AttackerRadius { get { return mImpl.mAttackerRadius; } }
	public Vector3 AttackerPosition { get { return mImpl.mAttackerPosition; } }
	public float AttackerDirection { get { return mImpl.mAttackerDirection; } }
	public uint AttackerLevel { get { return mImpl.mLevel; } }
	public float AttackerDamage { get { return mImpl.mDamage; } }
	public uint CriticalLevel { get { return mImpl.mCriticalLevel; } }

	public uint SummonerID { get { return mImpl.mSummonerID; } }
	public string SummonerName { get { return mImpl.mSummonerName; } }
	public uint SummonerLevel { get { return mImpl.mSummonerLevel; } }
	/// <summary>
	/// 技能效果的起始位置.
	/// </summary>
	public Vector3 EffectStartPosition { get { return mImpl.mEffectStartPosition; } }
	/// <summary>
	/// <para>技能效果的起始方向.</para>
	/// <para>作为爆炸效果时, 即起始位置 -> 目标位置的射线方向, 该值会被多次使用, 因此保存该值避免多次计算.</para>
	/// 作为射击碰撞时, 为子弹的飞行方向.
	/// </summary>
	public float EffectStartDirection { get { return mImpl.mEffectStartDirection; } }
	#endregion

	/// <summary>
	/// 设置技能效果的起始位置以及方向(该过程会触发写时复制, 从而使内部指针指向一个新的内部数据对象).
	/// </summary>
	public void SetEffectStartLocation(Vector3 position, float startDirection)
	{
		copyOnWrite();
		mImpl.mEffectStartPosition = position;
		mImpl.mEffectStartDirection = startDirection;
	}

	/// <summary>
	/// <para>该结构通过拷贝传递, 但是内部的数据通过指针传递.</para>
	/// 修改上述可写属性时, 拷贝新的内部数据, 从而使拷贝生成的多个该结构, 可以保存不同的可写数据.
	/// </summary>
	/// <remarks>当mImpl的引用计数为1时, 无需拷贝</remarks>
	void copyOnWrite()
	{
		mImpl = mImpl.ShallowClone();
	}
}

/// <summary>
/// AttackerAttr的数据.
/// </summary>
internal class AttackerAttrImpl
{
	/// <summary>
	/// 初始化该结构.
	/// </summary>
	/// <param name="attacker">技能效果等的发起者.</param>
	/// <param name="skillCommonID">发起者使用的技能ID(非技能发起填入uint.MaxValue).</param>
	/// <param name="structMadeByRandEvent">该结构是否由randevent创造(检测并避免randevent循环产生效果).</param>
	public AttackerAttrImpl(BattleUnit attacker, uint skillCommonID, bool structMadeByRandEvent)
	{
		mStructMadeByRandEvent = structMadeByRandEvent;
		mSkillCommonID = skillCommonID;
		if (attacker != null)
		{
			mAttackerID = attacker.InstanceID;
			mAttackerName = attacker.GetName();
			mAttackerRadius = attacker.GetRadius();
			mAttackerLeague = attacker.GetLeague();
			mEffectStartPosition = mAttackerPosition = attacker.GetPosition();
			mEffectStartDirection = mAttackerDirection = attacker.GetDirection();
			mLevel = attacker.GetLevel();
			mDamage = (uint)attacker.GetPropertyValue((int)PropertyTypeEnum.PropertyTypeDamage);
			mCriticalLevel = (uint)attacker.GetPropertyValue((int)PropertyTypeEnum.PropertyTypeCrticalLV);

			mSummonerID = attacker.SummonerID;
			mSummonerName = attacker.SummonerName;
			mSummonerLevel = attacker.SummonerLevel;
		}
	}

	/// <summary>
	/// 浅拷贝所有成员到新的对象中.
	/// </summary>
	public AttackerAttrImpl ShallowClone()
	{
		return (AttackerAttrImpl)MemberwiseClone();
	}

	// 该结构是否由randevent创造. 用来区分由随机事件发起的随机事件.
	public bool mStructMadeByRandEvent = false;

	// 技能ID.
	public uint mSkillCommonID = uint.MaxValue;

	// 效果的起始点, 默认为技能发起者的位置.
	// 但是在某些情况下, 比如子弹爆炸时, 对周围目标产生伤害, 那么该点为子弹的当前位置.
	public Vector3 mEffectStartPosition = Vector3.zero;
	public float mEffectStartDirection = 0f;

	// 最初发起者的属性.
	public uint mAttackerID = uint.MaxValue;

	// 发起者的半径.
	public float mAttackerRadius = 0f;

	// 发起者的名字.
	public string mAttackerName = "";

	// 发起者的阵营.
	public LeagueDef mAttackerLeague = LeagueDef.InvalidLeague;

	// 发起者发起时的位置.
	public Vector3 mAttackerPosition = Vector3.zero;

	// 效果的产生方向. 默认为技能发起者的方向.
	// 但是在某些情况下, 比如子弹飞行命中, 该值改为子弹的飞行方向.
	public float mAttackerDirection = 0f;

	// 等级.
	public uint mLevel = 1;

	// 攻击力.
	public uint mDamage = 0;

	// 暴击等级.
	public uint mCriticalLevel = 1;

	public uint mSummonerID = uint.MaxValue;
	public string mSummonerName = string.Empty;
	public uint mSummonerLevel = 1;
}
