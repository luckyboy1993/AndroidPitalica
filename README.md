# AndroidPitalica

adding questionResults: -> called upon submitting answer

[POST]/api/Questions: 
  userId (int), 
  examId (int), 
  questionId (int), 
  answered (string), -> users answer to given question
  correctAnswer (string), -> correct answer to given question
  score (int) -> if answered!=correctAnswer -> should be 0
  
  
adding examsTaken: -> called upon openning exam

[POST]/api/UserExamTakens: 
  userId (int), 
  examId (int)
  
getting exam results: -> called upon ending exam

[POST]/api/GetExamResults: 
  userId (int), 
  examId (int)
