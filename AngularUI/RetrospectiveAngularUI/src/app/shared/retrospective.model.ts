export class Retrospective {
     name: string;
     summary: string;
     date: string;
     participants: string[];  
     feedback: 
          [
               {
                    name: string;
                    body: string;
                    feedbacktype: string;
               }
          ]
}
