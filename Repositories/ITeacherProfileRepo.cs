using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface ITeacherProfileRepo : IProfileRepo<TeacherProfileEntity, TeacherProfileModel>;