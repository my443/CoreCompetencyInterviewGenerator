using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewGeneratorBlazorHybrid.Helpers
{
    public static class SampleDatabaseHelper
    {
        /** Categories and questions are selected from: https://dscorecomp.com/behavior-based-interviews/hiring-a-support-professional/ **/
        public static string CategoriesInsertQuery = @"
        DELETE FROM Categories;
        INSERT INTO ""Categories"" (""Id"", ""Name"", ""Description"") VALUES
          (1, 'Advocacy', 'Questions to assess influencing, persuasion, advocating ideas or change'),
          (2, 'Building Relationships', 'Questions about collaborating, interacting across stakeholders, relationship building'),
          (3, 'Championing Change and Innovation', 'Questions about embracing change, innovating, adapting'),
          (4, 'Facilitate Growth and Development', 'Questions about coaching, mentoring, developing others'),
          (5, 'Inclusive Leadership', 'Questions about ethics, leadership, inclusion, values'),
          (6, 'Problem Solving & Decision Making', 'Questions about analysis, judgment, creative solutions'),
          (7, 'Resilience', 'Questions about overcoming challenges, maintaining focus under pressure, and recovering from setbacks'),
          (8, 'Resource Management', 'Questions about prioritizing, allocating, and optimizing use of time, people, and materials'),
          (9, 'Strategic Thinking', 'Questions about long-term vision, planning, anticipating trends, and aligning actions with goals'),
          (10, 'Valuing Equity Diversity and Inclusion (EDI)', 'Questions about fostering fairness, respect, belonging, and appreciating diverse perspectives');";

        public static string QuestionsInsertQuery = @"
        DELETE FROM Questions;
        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Tell me about a time when you advocated for an idea, position or approach', ' ', 1),
          ('Describe a recent situation in which you convinced an individual or a group to do something.', ' ', 1),
          ('Can you tell me about a time when you had to take several steps to persuade an individual or group?', ' ', 1),
          ('Tell me about a time when you had to sell an idea to several individuals or groups.', ' ', 1),
          ('Describe a time when you considered others’ perspectives in your efforts to convince or persuade them.', ' ', 1),
          ('Tell me about a time when you used multiple influence strategies to achieve an outcome.', ' ', 1),
          ('Tell me about a time when you used a complex influencing strategy to create a desired impression or impact, or to reach a goal.', ' ', 1),
          ('Can you describe a situation when you used a highly sophisticated influencing strategy to bring about sustainable change?', ' ', 1);
  
        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Tell me about a time when you collaborated with others to make specific decisions or plans.', ' ', 2),
          ( 'Can you tell me about a time when you were involved in a challenging or difficult team situation?', ' ', 2),
          ('Tell me about a time when it was important but challenging for you to interact with different members of a group/team. Specifically, how did you go about it?', ' ', 2),
          ( 'Describe a time when you had to build relationships or links with different departments across an organization/agency. How did you go about it?', ' ', 2),
          ( 'Can you tell me about a time when you collaborated with others outside the organization/agency, for example, to share knowledge, build capacity, and/or come up with original approaches to meeting challenging goals?', ' ', 2),
          ( 'Describe a situation when you worked collaboratively with other agencies or organizations to further the goals of the agency/organization/sector.', ' ', 2),
          ( 'Tell me about a time when you pursued a friendly relationship to ensure a positive working environment.', ' ', 2),
          ( 'Can you tell me about a time when you intentionally chose to establish a positive, collegial relationship with a new team member or employee?', ' ', 2),
          ( 'Can you tell me about a time where you felt particularly effective in establishing a business relationship with a client/customer?', ' ', 2),
          ( 'Describe a time when you relied on a contact in your network to help you with a work-related task or problem.', ' ', 2),
          ('Tell me about a professional relationship you have established, and a situation where you used that relationship to deal with a work-related task or problem.', ' ', 2),
          ('Describe a situation where you used your network of contacts to benefit the agency or organization.', ' ', 2);

        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Can you tell me about a time when a situation you had planned for did not turn out the way you expected?', ' ', 3),
          ('What do you do when priorities change quickly? Give one example of when this happened.', ' ', 3),
          ('Describe a time when you altered your own behaviour to fit the situation?', ' ', 3),
          ('Tell us about a time that you had to adapt to a difficult situation.', ' ', 3),
          ('Describe a major change that occurred in a job that you held. How did you adapt to this change?', ' ', 3),
          ('Give me an example about a time when your priorities were changed by someone else and you didn’t have much warning about it.', ' ', 3),
          ('Tell me about a time when you had to change your perspective or plans to take into account new information or changing priorities.', ' ', 3),
          ('Describe a time when you adjusted your approach to a problem or issued based on new or changing information.', ' ', 3),
          ('Can you tell me about a time when you changed your approach or priorities to meet others’ expectations?', ' ', 3),
          ('Tell me about a time when you had to change your plans or activities because of an expected event. What did you do?', ' ', 3),
          ('Can you describe a time when you had to change your whole approach and start over again in order to get something done?', ' ', 3),
          ('Can you tell me about a time when you had to communicate a change to your team?', ' ', 3),
          ('Describe a challenging change initiative you helped to implement. What did you do to ensure its success?', ' ', 3),
          ('Give an example of a time when you helped a staff member accept change and make the necessary adjustments to move forward. What were the change/transition skills that you used?', ' ', 3),
          ('Tell me about a time when you helped others understand or accept a (substantial/significant) change in their work or work environment.', ' ', 3),
          ('Can you tell me about a time when you were part of a significant change initiative? What did you do to make it real for people?', ' ', 3),
          ('Tell me about a time when you were working on a change initiative and had to involve others.', ' ', 3),
          ('Tell us about a time when you anticipated the future and made changes to current responsibilities/operations to meet future needs.', ' ', 3),
          ('Can you tell me about a change initiative you were implementing and how you prepared people for the change?', ' ', 3),
          ('Describe a change initiative you were responsible for implementing. How did you gain commitment of your people to the change', ' ', 3);
  
        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Can you tell me about a time when you provided support and encouragement to someone?', ' ', 4),
          ('Tell me about a time when you had to foster independence in someone? How did you do this?', ' ', 4),
          ('Can you tell me about a time when you tried to fully empower another person to do something?', ' ', 4),
          ('There are times when people need extra help. Give an example of when you were able to provide that support to a person with whom you worked.', ' ', 4),
          ('Describe a time when you provided instruction or guidance to someone in a supportive manner.', ' ', 4),
          ('Have you ever had a subordinate whose performance was consistently marginal? What did you do?', ' ', 4),
          ('Can you tell me about a time when you delegated a task to someone? How did you go about doing this?', ' ', 4),
          ('Tell me about a time when you transitioned responsibility of a task to someone else.', ' ', 4),
          ('Discuss a situation in which you had to provide guidance to an employee or co-worker on performance issues related to work. What was your approach? What difficulties did you face? What solutions did you implement?', ' ', 4),
          ('Describe a time when it was important to you to empower another person. How did you do this?', ' ', 4),
          ('Can you tell me about a time when you encouraged others to delegate or empower others?', ' ', 4),
          ('Tell me about a time when you coached an employee or peer around a task or assignment.', ' ', 4),
          ('Can you tell me about someone who became successful as a result of the steps you took to coach/develop them?', ' ', 4),
          ('Describe a time when you feel you contributed to someone’s ability to take on a task and work independently.', ' ', 4),
          ('Tell me about a time when you had an opportunity to develop the knowledge, skills or abilities of another person.', ' ', 4),
          ('Tell me about a time when you provided feedback to someone about their performance (for developmental purposes).', ' ', 4),
          ('Describe a time when you provided coaching and support around an individual’s long-term development.', ' ', 4),
          ('Can you tell me about a time when you took steps to create a supportive learning environment to facilitate growth and development?', ' ', 4);  
  
        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Can you tell me about a time when you had to take an action that was consistent with what you thought was important, or, in other words, had to “walk the talk”?', ' ', 5),
          ('Tell me about a difficult situation in which you behaved in a way that was consistent with your values.', ' ', 5),
          ('Describe a time when you realized that a decision you made was incorrect.', ' ', 5),
          ('Can you give me an example of a time when you challenged a group’s or individual’s actions because you felt they were negatively impacting the agency/organization?', ' ', 5),
          ('Tell me about a time when you challenged a directive or course of action that did not align with professional values and ethics.', ' ', 5),
          ('Tell us about the most difficult situation you have had when leading a team. What happened and what did you do? Was it successful? Emphasize the “single” most important thing you did?', ' ', 5),
          ('Tell me about a situation when you had to deal with a direct report who wanted something that you felt was unreasonable. How did you handle that situation.', ' ', 5),
          ('Can you describe a time when you set and communicated clear performance goals, measures and perspectives?', ' ', 5),
          ('Give an example of how you have been successful at empowering a group of people in accomplishing a task.', ' ', 5),
          ('Describe a time when you recognized that a member of your team had a performance deficiency/difficulty.', ' ', 5),
          ('Can you tell me about a time when the details of a task you had assigned to someone slipped through the cracks? What happened and what did you do?', ' ', 5),
          ('Describe a time when you felt an employee was not meeting the performance standards. What did you do?', ' ', 5),
          ('Tell me about a time when you confronted a co-worker /direct report about a performance issue.', ' ', 5),
          ('Can you tell me about a time when you had to deal with an employee with a serious performance issue where termination was a possibility?', ' ', 5),
          ('Can you tell me about a time when you took on a leadership role?', ' ', 5),
          ('Describe a time when you had to lead a group to achieve an objective.', ' ', 5),
          ('Can you tell me about a time when you had to lead a group of people to work together effectively?', ' ', 5),
          ('Can you give me an example of a time when you helped your team deal with a difficult decision or situation?', ' ', 5),
          ('Tell me about a time when you got others to “buy-in” to your mission, goals or strategy. What did you do?', ' ', 5),
          ('Tell me about a time when you generated excitement, enthusiasm and commitment in people to your vision', ' ', 5),
          ('Tell us about a time when you used your leadership ability to gain support for what initially had strong opposition.', ' ', 5);

        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Tell me about a time when you had to solve a problem or make a decision.', ' ', 6),
          ('Can you tell me about a time when you identified a new, unusual or different approach to addressing a problem or decision?', ' ', 6),
          ('Tell me about a recent problem to which old solutions wouldn’t work. How did you solve the problem?', ' ', 6),
          ('Describe a situation in which you had to come up with a creative or unique solution to a problem.', ' ', 6),
          ('Can you tell me about a situation where you had to solve a problem or make a decision that required careful thought? What did you do?', ' ', 6),
          ('On many occasions, managers have to make tough decisions. What was the most difficult one you have had to make?', ' ', 6),
          ('Tell me about the most challenging situation you have had to analyze and make a decision on.', ' ', 6);

        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Give me an example of a recent situation that you found very stressful.', ' ', 7),
          ('Describe a time when you were in a high pressure situation. What was the situation and what did you do?', ' ', 7),
          ('Describe a situation when you had to exercise a significant amount of self-control.', ' ', 7),
          ('Can you tell me about a situation where you were confronted with opposition or hostility? How did you handle it?', ' ', 7),
          ('Tell me about a time when you found yourself in a challenging interpersonal situation.', ' ', 7),
          ('Sometimes we need to remain calm on the outside when we are really upset on the inside. Give an example of a time that this happened to you.', ' ', 7),
          ('There are times when we are placed under extreme pressure on the job. Tell about a time when you were under such pressure and how you handled it.', ' ', 7),
          ('Describe a time when you had to deal with an especially difficult or angry individual or client?', ' ', 7),
          ('Give an example of a time in which you felt you were able to build motivation in your co-workers or subordinates when they were feeling defeated or de-motivated.', ' ', 7),
          ('Tell me about a situation where, despite significant pressure or stress, you were able to maintain a positive outlook.', ' ', 7),
          ('Can you describe a time when you had to maintain your motivation and stamina while under pressure or trying conditions?', ' ', 7),
          ('Describe a situation where you continued your efforts even though it may have been easier to give up?', ' ', 7),
          ('Tell me about a time when you had to overcome obstacles to complete a task or achieve an objective.', ' ', 7),
          ('Describe a time when you stuck with a task despite repeated rejection and/or frustration?', ' ', 7),
          ('Have you ever met resistance when implementing a new idea or policy to a work group? How did you deal with it? What happened?', ' ', 7),
          ('Can you recall a situation where you were faced with routine or repetitive tasks over a long period of time yet were able to maintain motivation?', ' ', 7);

        -- Category 8: Resource Management
        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Tell me about a time when you had to deal with a particular resource management issue.', ' ', 8),
          ('Describe a time when you had to obtain ongoing information and feedback about resource utilization to make a timely and effective decision.', ' ', 8),
          ('Tell me about a time when you identified gaps and suggested improvement or makes recommendations regarding resource management to decision makers', ' ', 8),
          ('Can you tell me about a time when you had to improve the effective utilization of resources even when this required having your group/team make “sacrifices”?', ' ', 8);

        -- Category 9: Strategic Thinking
        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Describe a time when you took initiative to respond to a problem or opportunity.', ' ', 9),
          ('Tell me about a time when you thought ahead and planned for future challenges.', ' ', 9),
          ('Give an example of when you anticipated a future issue or trend and took steps to prepare for it.', ' ', 9),
          ('Describe a situation in which you aligned your work or decision with longer-term goals or vision.', ' ', 9),
          ('Tell me about a time when you challenged yourself or your team to think differently or more strategically.', ' ', 9);

        -- Category 10: Valuing Equity, Diversity & Inclusion (EDI)
        INSERT INTO ""Questions"" (""Text"", ""Answer"", ""CategoryId"") VALUES
          ('Can you tell me about a time when you acted in a way that showed respect for diversity or inclusion?', ' ', 10),
          ('Give an example of how you have made efforts to include diverse voices or perspectives in a decision or process.', ' ', 10),
          ('Describe a time when you recognized your own bias and took steps to overcome it.', ' ', 10),
          ('Tell me about a time when you confronted or challenged exclusionary or discriminatory behavior.', ' ', 10),
          ('Can you describe a time when you advocated for equity or fairness in a situation?', ' ', 10),
          ('Describe a time when your actions helped someone from a marginalized or underrepresented group.', ' ', 10),
          ('Tell me about a time when you created a sense of belonging for someone who felt excluded.', ' ', 10);";

        public static string InterviewsInsertQuery = @"
            INSERT INTO ""main"".""Interviews"" (""Id"", ""InterviewName"", ""DateCreated"", ""IsActive"") VALUES (1, 'DSP Interview for Group Living', '2025-11-02 00:00:00', 1);
            INSERT INTO ""main"".""Interviews"" (""Id"", ""InterviewName"", ""DateCreated"", ""IsActive"") VALUES (2, 'Night Sleep DSP Interview', '2025-11-02 00:00:00', 1);";

        public static string InterviewQuestionsInsertQuery = @"
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (1, 1);
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (1, 10);
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (1, 68);
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (1, 117);
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (2, 1);
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (2, 105);
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (2, 100);
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (2, 10);
            INSERT INTO ""main"".""InterviewQuestion"" (""InterviewsId"", ""QuestionsId"") VALUES (2, 67);
            ";

    }
}
