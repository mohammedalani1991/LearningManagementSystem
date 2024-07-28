alter table SenangPay add EnrollTeacherCourseId int

ALTER TABLE SenangPay
ADD FOREIGN KEY (EnrollTeacherCourseId) REFERENCES EnrollTeacherCourse(Id);