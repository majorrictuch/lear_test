namespace Lear.CRS.Model
{
	public class ConnectionDb
    {
        /// <summary>
        /// 默认数据库编号
        /// </summary>
        public string DefaultDbNumber { get; set; }
        /// <summary>
        /// 默认数据库类型
        /// </summary>
        public string DefaultDbType { get; set; }
        /// <summary>
        /// 默认数据库连接字符串
        /// </summary>

        public string DefaultDbString { get; set; }
        /// <summary>
        /// 业务库集合
        /// </summary>
        public DbConfig[] DbConfigs { get; set; }
    }
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// 数据库编号
        /// </summary>
        public string DbNumber { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>

        public string DbString { get; set; }
    }
}
