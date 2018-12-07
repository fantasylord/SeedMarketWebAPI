namespace CoreBackend.Api.Dtos
{
    /// <summary>
    /// 农作物分类
    /// </summary>
    public enum SeedClass
    {
        无类别 = 0,
        蔬菜菌类,
        瓜果类,
        花草林木类,
        大田作物类,
        全部分类
    }
    /// <summary>
    /// 种子类别
    /// </summary>
    public enum SpeciesClass { 
        无类别 = 0,
        草花,
        种球,
        叶菜类,
        牧草,
        草坪草,
        瓜类,
        辣椒,
        宿根花卉
    }
    /// <summary>
    /// 状态枚举类
    /// </summary>
    public enum IndentStatus 
    {
        所有状态 = 0,
        处理中,
        完成,
        关闭,
        异常,
        创建
        //  0="创建"，1="处理中"，2=完成，3=关闭，4=异常
    }

    public enum BusinessStatus 
    {
        异常 = 0,
        营业中,
        停业
    }
    public enum FileClass 
    {
        个人图片 = 0,
        标识图片,
        展示图片,
        文本,
        视频,

        //  0="创建"，1="处理中"，2=完成，3=关闭，4=异常
    }
}
