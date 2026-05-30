# AI Evaluation

## Tested Prompts

### 1. Reschedule a Doctor's Appointment
Prompt:
"Jag vill boka läkartid för astma uppföljning"

### 2. School Absence Due to Illness
Prompt:
"Jag vill sjukanmäla mig imorgon, grov förskylning"

### 3. Postpone a Meeting Until Next Wednesday
Prompt:
"Jag vill skjuta upp mötet till nästa onsdag, vabbar idag"

### 4. Change a Flight Ticket to Next Week
Prompt:
"Jag vill boka om flygresa till nästa vecka”


## Response Quality

The AI generated clear, relevant, and well-structured email content based on the provided prompts. The responses generally matched the requested tone and subject while maintaining readability and consistency.


## Limitations

The quality of generated content depends on the prompt provided. Responses may vary in accuracy, detail, and relevance depending on the input and the external AI service used.


## Test Prompts

- Write a professional email to reschedule a doctor's appointment.
- Write a school absence email due to illness.
- Write an email to postpone a meeting until next Wednesday.
- Write an email requesting to change a flight ticket to next week.

All test prompts were executed through the /api/Emails/generate endpoint and returned HTTP 200 OK responses.

## Results

### Positive Results
- Generated relevant and well-structured emails.
- Adapted the response based on the requested tone.
- Produced useful content for different email scenarios.

### Negative Results
- Response quality depended on the prompt details.
- Some responses required minor manual adjustments.
- External AI service availability could affect results.

## Conclusion

The AI Content Assistant successfully generated email content for multiple scenarios and handled both successful and failed requests. The implemented middleware improved error handling, while testing verified that the system responded correctly under different conditions.




