using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Odigo.Business.Interfaces;
using Odigo.Model.Model;
using Odigo.Entities;
using System.Linq.Expressions;
using Odigo.Model.Translator;
using System.Data.Entity;

namespace Odigo.Business
{
    public class TeacherFinderService : ITeacherFinder
    {
        private readonly IRepository _da;
        private readonly IModelAggregator<TeacherStudentCategory> _teacherChildAggregator;
        private readonly IModelAggregator<TeacherEmploymentHistory> _teacherExperienceAggregator;
        private readonly IModelAggregator<TeacherEducationalQualification> _teacherQualificationAggregator;
        
        public TeacherFinderService(IRepository da, IModelAggregator<TeacherEducationalQualification> teacherQualificationAggregator, IModelAggregator<TeacherStudentCategory> teacherChildAggregator, IModelAggregator<TeacherEmploymentHistory> teacherExperienceAggregator)
        {
            if (da == null)
            {
                throw new ArgumentNullException("da");
            }
            if (teacherChildAggregator == null)
            {
                throw new ArgumentNullException("teacherChildAggregator");
            }
            if (teacherExperienceAggregator == null)
            {
                throw new ArgumentNullException("teacherExperienceAggregator");
            }
            if (teacherQualificationAggregator == null)
            {
                throw new ArgumentNullException("teacherQualificationAggregator");
            }

            _da = da;            
            _teacherChildAggregator = teacherChildAggregator;
            _teacherExperienceAggregator = teacherExperienceAggregator;
            _teacherQualificationAggregator = teacherQualificationAggregator;
        }

        public Teacher GetBy(Person person)
        {
            try
            {
                TEACHER entity = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.Person_Id == person.Id
                                          select teacher).SingleOrDefault();

                Teacher tutor = Translate(entity);
                return GetByHelper(tutor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetBy(State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                                where teacher.PERSON.State_Id == state.Id
                                                select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Teacher>> GetByAsync(State state)
        {
            try
            {
                List<TEACHER> entities = await (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          select teacher).ToListAsync();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Teacher> GetBy(Qualification qualification)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.Qualification_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetBy(QualificationCategory qualificationCategory)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.QUALIFICATION.Qualification_Category_Id == qualificationCategory.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetTopBy(int top, Expression<Func<TEACHER, bool>> selector)
        {
            try
            {
                Func<IQueryable<TEACHER>, IOrderedQueryable<TEACHER>> orderBy = p => p.OrderByDescending(s => s.Person_Id);
                List<Teacher> teachers = _da.GetTopBy<Teacher, TEACHER>(top, selector, orderBy);

                return GetByHelper(teachers);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task<List<Teacher>> GetTopByAsync(int top, Expression<Func<TEACHER, bool>> selector)
        {
            try
            {
                Func<IQueryable<TEACHER>, IOrderedQueryable<TEACHER>> orderBy = p => p.OrderByDescending(s => s.Person_Id);
                List<Teacher> teachers = await _da.GetTopByAsync<Teacher, TEACHER>(top, selector, orderBy);

                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetBy(StudentCategory childCategory)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(TeacherType teacherType)
        {
            try
            {
                List<Teacher> teachers = _da.GetModelsBy<Teacher, TEACHER>(t => t.Teacher_Type_Id == teacherType.Id);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(StudentCategory childCategory, Qualification qualification)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.Qualification_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetBy(StudentCategory childCategory, QualificationCategory qualification)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.QUALIFICATION.Qualification_Category_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(StudentCategory childCategory, State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          && teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(Qualification qualification, State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.Qualification_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetBy(QualificationCategory qualification, State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.QUALIFICATION.Qualification_Category_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(TeacherType teacherType, State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          && teacher.Teacher_Type_Id == teacherType.Id
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(TeacherType teacherType, Qualification qualification)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.Teacher_Type_Id == teacherType.Id
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.Qualification_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetBy(TeacherType teacherType, QualificationCategory qualification)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.Teacher_Type_Id == teacherType.Id
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.QUALIFICATION.Qualification_Category_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(TeacherType teacherType, StudentCategory childCategory)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.Teacher_Type_Id == teacherType.Id
                                          && teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(StudentCategory childCategory, QualificationCategory qualification, State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          && teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.QUALIFICATION.Qualification_Category_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetBy(TeacherType teacherType, QualificationCategory qualification, State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          && teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.TEACHER.Teacher_Type_Id == teacherType.Id)
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.QUALIFICATION.Qualification_Category_Id == qualification.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Teacher> GetBy(TeacherType teacherType, StudentCategory childCategory, State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          && teacher.Teacher_Type_Id == teacherType.Id
                                          && teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          select teacher).ToList();

                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(TeacherType teacherType, StudentCategory childCategory, QualificationCategory qualification)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.Teacher_Type_Id == teacherType.Id
                                          && teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.QUALIFICATION.Qualification_Category_Id == qualification.Id)
                                          select teacher).ToList();


                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teacher> GetBy(TeacherType teacherType, StudentCategory childCategory, QualificationCategory qualification, State state)
        {
            try
            {
                List<TEACHER> entities = (from teacher in _da.DbContext.Set<TEACHER>()
                                          where teacher.PERSON.State_Id == state.Id
                                          && teacher.Teacher_Type_Id == teacherType.Id
                                          && teacher.TEACHER_STUDENT_CATEGORY.Any(x => x.Student_Category_Id == childCategory.Id)
                                          && teacher.TEACHER_EDUCATIONAL_QUALIFICATION.Any(x => x.QUALIFICATION.Qualification_Category_Id == qualification.Id)
                                          select teacher).ToList();
                
                List<Teacher> teachers = Translate(entities);
                return GetByHelper(teachers);
            }
            catch (Exception)
            {
                throw;
            }
        }
               
        private List<Teacher> Translate(List<TEACHER> entities)
        {
            try
            {
                if (entities == null || entities.Count <= 0)
                {
                    return null;
                }

                Type type = entities[0].GetType().BaseType;
                if (type.Equals(typeof(object)))
                {
                    type = entities[0].GetType();
                }

                dynamic translator = TranslatorFactory.Create(type.Name);
                if (translator == null)
                {
                    throw new Exception("translator");
                }

                return translator.Translate(entities);
            }
            catch(Exception)
            {
                throw;
            }
        }
        private Teacher Translate(TEACHER entity)
        {
            try
            {
                if (entity == null || entity.Person_Id <= 0)
                {
                    return null;
                }

                Type type = entity.GetType().BaseType;
                if (type.Equals(typeof(object)))
                {
                    type = entity.GetType();
                }

                dynamic translator = TranslatorFactory.Create(type.Name);
                if (translator == null)
                {
                    throw new Exception("translator");
                }

                return translator.Translate(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<Teacher> GetByHelper(List<Teacher> teachers)
        {
            try
            {
                if (teachers != null && teachers.Count > 0)
                {
                    foreach (Teacher teacher in teachers)
                    {
                        GetByHelper(teacher);


                        //if (teacher.EducationalQualifications != null && teacher.EducationalQualifications.Count > 0)
                        //{
                        //    teacher.Qualifications = _teacherQualificationAggregator.Aggregate(teacher.EducationalQualifications);
                        //}

                        //if (teacher.StudentCategories != null && teacher.StudentCategories.Count > 0)
                        //{
                        //    teacher.ChildCategories = _teacherChildAggregator.Aggregate(teacher.StudentCategories);
                        //}

                        //if (teacher.EmploymentHistories != null && teacher.EmploymentHistories.Count > 0)
                        //{
                        //    teacher.Experience = _teacherExperienceAggregator.Aggregate(teacher.EmploymentHistories);
                        //}
                    }
                }

                return teachers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Teacher GetByHelper(Teacher teacher)
        {
            try
            {
                if (teacher != null && teacher.Person != null && teacher.Person.Id > 0)
                {
                        if (teacher.EducationalQualifications != null && teacher.EducationalQualifications.Count > 0)
                        {
                            teacher.Qualifications = _teacherQualificationAggregator.Aggregate(teacher.EducationalQualifications);
                        }

                        if (teacher.StudentCategories != null && teacher.StudentCategories.Count > 0)
                        {
                            teacher.ChildCategories = _teacherChildAggregator.Aggregate(teacher.StudentCategories);
                        }

                        if (teacher.EmploymentHistories != null && teacher.EmploymentHistories.Count > 0)
                        {
                            teacher.Experience = _teacherExperienceAggregator.Aggregate(teacher.EmploymentHistories);
                        }
                }

                return teacher;
            }
            catch (Exception)
            {
                throw;
            }
        }





        //public List<Teacher> Get()
        //{
        //    try
        //    {

        //        List<Teacher> teachers = _da.Get();
        //        return GetByHelper(teachers);



        //        //from parent in parents where parent.Children.Any(c => c.CategoryNumber == 1) select parent into p
        //        //from child in p.Children where child.CategoryNumber == 2 select child

        //        //from parent in parents where parent.Children.Any(c => c.CategoryNumber == 1) select parent into p
        //        //from child in p.Children where child.CategoryNumber == 2 select child
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



    }


}
