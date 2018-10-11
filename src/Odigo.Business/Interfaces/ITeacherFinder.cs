using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Model.Model;
using System.Linq.Expressions;
using Odigo.Entities;

namespace Odigo.Business.Interfaces
{
    public interface ITeacherFinder
    {
        Teacher GetBy(Person person);

        List<Teacher> GetBy(TeacherType teacherType);
        List<Teacher> GetBy(StudentCategory childCategory);
        List<Teacher> GetBy(Qualification qualification);
        List<Teacher> GetBy(QualificationCategory qualificationCategory);
        List<Teacher> GetBy(State state);

        List<Teacher> GetTopBy(int top, Expression<Func<TEACHER, bool>> selector);
        List<Teacher> GetBy(TeacherType teacherType, StudentCategory childCategory);
        List<Teacher> GetBy(TeacherType teacherType, Qualification qualification);
        List<Teacher> GetBy(TeacherType teacherType, QualificationCategory qualification);
        List<Teacher> GetBy(TeacherType teacherType, State state);
        List<Teacher> GetBy(StudentCategory childCategory, Qualification qualification);
        List<Teacher> GetBy(StudentCategory childCategory, QualificationCategory qualification);
        List<Teacher> GetBy(StudentCategory childCategory, State state);
        List<Teacher> GetBy(Qualification qualification, State state);
        List<Teacher> GetBy(QualificationCategory qualification, State state);
        
        List<Teacher> GetBy(TeacherType teacherType, StudentCategory childCategory, QualificationCategory qualification);
        List<Teacher> GetBy(TeacherType teacherType, StudentCategory childCategory, State state);
        List<Teacher> GetBy(StudentCategory childCategory, QualificationCategory qualification, State state);
        List<Teacher> GetBy(TeacherType teacherType, QualificationCategory qualification, State state);

        List<Teacher> GetBy(TeacherType teacherType, StudentCategory childCategory, QualificationCategory qualification, State state);



        Task<List<Teacher>> GetByAsync(State state);
        Task<List<Teacher>> GetTopByAsync(int top, Expression<Func<TEACHER, bool>> selector);
    }





}
