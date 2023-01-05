namespace WH.ADMIN.Models
{
    public static class Status
    {
        public const string PENDING = "P";
        public const string INACTIVE = "I";
        public const string ACTIVE = "A";
        public const string DELETED = "D";
        public const string SUCCESS = "S";
    }

    public static class Roles
    {
        public const long ADMIN = 1;
        public const long OPERATOR = 2;
        public const long SUPERADMIN = 3;
    }

    public static class ResponseMessages
    {
        public const string ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION = "Only an admin can perform this action.";

    }
}
