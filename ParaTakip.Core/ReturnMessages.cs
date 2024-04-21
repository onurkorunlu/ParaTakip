namespace ParaTakip.Core
{
    public static class ReturnMessages
    {

        #region SYSTEM
        public const string SUCCESSFUL = "İşlem başarılı.";
        public const string GENERIC_ERROR = "Beklenmeyen bir hata oluştu.";
        public const string MODEL_VALIDATION_ERROR = "Girilen değerler hatalı, lütfen kontrol ederek tekrar giriniz. </br></br> {0}.";
        public const string INVALID_PARAMETER = "Hatalı parametre. '{0}'";
        public const string INVALID_PARAMETER_USERNAME_LIST = "Kullanıcı adları alt alta yazılmalıdır.";
        public const string INVALID_USERNAME_OR_PASSWORD = "Kullanıcı adı veya şifre hatalı.";
        public const string INVALID_USERNAME_CHARACTER_LENGHT = "Kullanıcı adınız 5-50 karakter arasında olmalıdır.";
        public const string INVALID_PASSWORD_CHARACTER_LENGHT = "Parolanız 5 karakterdan az olamaz.";
        public const string INVALID_EMAIL_FORMAT = "Lütfen geçerli bir e-posta adresi giriniz.";
        public const string NOT_ALLOWED_REQUEST = "Bu istek yetkisizdir.";
        public const string BATCH_LOGIN_ERROR = "BatchLogin error : {0}";
        #endregion

        #region OPERATIONS
        public const string CREATE_ERROR = "Kayıt işlemi yapılırken bir hata oluştu.";
        public const string UPDATE_ERROR = "Güncelleme işlemi yapılırken bir hata oluştu.";
        public const string DELETE_ERROR = "Silme işlemi yapılırken bir hata oluştu.";
        public const string CREATE_SUCCESSFUL = "Kayıt başarıyla eklendi.";
        public const string UPDATE_SUCCESSFUL = "Güncelleme işlemi başarıyla gerçekleştirildi.";
        public const string DELETE_SUCCESSFUL = "Silme işlemi başarıyla gerçekleştirildi.";
        #endregion

        #region DB & CONFIGURATIOn
        public const string CONNECTION_ERROR =  "Bağlantı kurulurken bir hata oluştu.";
        public const string MONGO_DB_ERROR =  $"Beklenmeyen bir hata oluştu.";
        public const string MONGO_DB_CONN_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde MongoDB bağlantı bilgisi bulunamadı.";
        public const string MONGO_DB_NAME_NOT_FOUND_IN_CONFIGURATION =  "Config içerisinde MongoDB veritabanı bilgisi bulunamadı.";
        public const string YOUTUBE_API_KEY_NOT_FOUND_IN_CONFIGURATION =  "Config içerisinde youtube api key bilgisi bulunamadı.";
        public const string YOUTUBE_SEARCH_COMMENT_API_KEY_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde search youtube comment api key bilgisi bulunamadı.";
        public const string GETTING_YOUTUBE_COMMENTS_FAILED = "Youtube yorumları alınırken hata oluştu.";
        public const string NEWS_API_KEY_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde News api key bilgisi bulunamadı.";
        public const string SECRET_KEY_NOT_FOUND = "Config içerisinde Secret Key bilgisi bulunamadı.";
        public const string EMAIL_PORT_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde Email Port bilgisi bulunamadı.";
        public const string EMAIL_SMTP_ADRESS_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde Email Smtp Adress bilgisi bulunamadı.";
        public const string SENDER_EMAIL_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde Sender Email bilgisi bulunamadı.";
        public const string SENDER_EMAIL_PASSWORD_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde Sender Email Password bilgisi bulunamadı.";
        public const string EMAIL_ENABLE_SSL_FLAG_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde Email Enable Ssl Flag bilgisi bulunamadı.";
        public const string RESET_PASSWORD_LINK_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde Reset Password Link bilgisi bulunamadı.";
        public const string JWT_SECRET_KEY_NOT_FOUND_IN_CONFIGURATION = "Config içerisinde JWT Secret Key bilgisi bulunamadı.";
        public const string EMAIL_TEMLATE_NOT_FOUND = "E-Mail şablonu bulunamadı. Şablon : {0}.";
        public const string EMAIL_PROVIDER_USERNAME_NOT_FOUND = "E-Mail kullanıcı adı bulunamadı.";
        public const string EMAIL_PROVIDER_PASSWORD_NOT_FOUND = "E-Mail şifresi bulunamadı.";
        public const string EMAIL_PROVIDER_HOST_NOT_FOUND = "E-Mail hostu bulunamadı.";
        public const string EMAIL_PROVIDER_FROM_NOT_FOUND = "E-Mail gönderici bulunamadı.";
        public const string EMAIL_PROVIDER_TO_NOT_FOUND = "E-Mail alıcı bulunamadı.";
        public const string EMAIL_PROVIDER_SUBJECT_NOT_FOUND = "E-Mail konusu bulunamadı.";
        public const string EMAIL_PROVIDER_PORT_NOT_FOUND = "E-Mail portu bulunamadı.";
        public const string MYSQL_CONNECTION_STATE_NOT_OPEN = "Mysql bağlantısı açılamadı.";
        public const string MYSQL_CONNECTION_ERROR = "Mysql bağlantı hatası.";
        #endregion

        #region BUSINESS

        public const string ITEM_NOT_FOUND = "Veri bulunamadı.";
        public const string APP_USER_ROLE_NOT_FOUND = "Rol bulunamadı.";
        public const string USER_ROLE_IS_UNAUTHORIZED = "Kullanıcnın bu sayfayı açmaya yetkisi yoktur.";
        public const string LOGIN_FAILED = "Giriş işlemi yapılırken hata oluştu : '{0}'";
        public const string USER_NOT_FOUND = "Kullanıcı bulunamadı.";
        public const string USER_ROLE_NOT_FOUND = "Kullanıcı rolü bulunamadı.";
        public const string SyncAllStarted = "Tarama işlemi başladı. Bu işlem yaklaşık 5 dakika sürecektir.";
        public const string BatchProcessAllAlreadyRunning = "Şuan çalışan bir tarama işlemi bulunmaktadır. Bu işlem bittikten sonra tekrar deneyebilirsiniz.";
        public const string PARAMETER_ALREADY_EXISTS = "Bu parametre zaten kayıtlıdır.";

        public const string WEALTH_NOT_FOUND = "Kullanıcı cüzdanı bulunamadı.";

        public const string USERNAME_ALREADY_EXISTS = "Kullanıcı adı zaten mevcut.";
        public const string EMAILADDRESS_ALREADY_EXISTS = "E-Posta adresi zaten mevcut.";
        #endregion

    }
}
