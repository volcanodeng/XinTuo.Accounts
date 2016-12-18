using AutoMapper;


namespace XinTuo.Accounts.AutoMapperProfiles
{
    public class CheckboxResolver : IMemberValueResolver<object, object, string, int>
    {
        public int Resolve(object source, object destination, string sourceMember, int destMember, ResolutionContext context)
        {
            return ((!string.IsNullOrEmpty(sourceMember) && sourceMember.ToLower() == "on") ? 1 : 0);
        }
    }

    public class ToCheckboxResolver : IMemberValueResolver<object, object, int, string>
    {
        public string Resolve(object source, object destination, int sourceMember, string destMember, ResolutionContext context)
        {
            return (sourceMember == 1 ? "on" : "off");
        }
    }
}