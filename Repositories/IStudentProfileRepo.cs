using PersonalAccount.Data.Entities;
using PersonalAccount.Models;

namespace PersonalAccount.Repositories;

public interface IStudentProfileRepo : IProfileRepo<StudentProfileEntity, StudentProfileModel>;