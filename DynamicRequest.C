Dynamic Number of request generation#include <stdio.h>
#include <stdlib.h>

void generateRequests(int CotractNumberCount) {
    for (int i = 1; i <= CotractNumberCount; i++) {
        printf("web_csubmit_data(\"getwrapEleigibility\",\n");
        printf("\"URL=www.example.com/test/wrap\",\n");
        printf("\"Method=POST\",\n");
        printf("ITEMDATA\n");
        printf("\"ContractNumber=ContractNumber_%d\",\n", i);
        printf("LAST);\n\n");
    }
}

int main() {
    int CotractNumberCount = 3;  // Change this value as per your requirement
    generateRequests(CotractNumberCount);
    return 0;
}


Dynamically add the rows inside request
#include <stdio.h>
#include <stdlib.h>

void generateRequest(int CotractNumberCount) {
    printf("web_url(\"Filterworklist\",\n");
    printf("\"URL=www.example.com\",\n");
    printf("\"Method=POST\",\n");
    printf("ITEMDATA\n");

    for (int i = 1; i <= CotractNumberCount; i++) {
        printf("\"Key=Contractnumber[]\", \"Value={contractnumber_%d}\", ENDITEM,\n", i);
    }

    printf("\"Key=address\", \"Value=1234\", ENDITEM,\n");
    printf("LAST);\n\n");
}

int main() {
    int CotractNumberCount = 3;  // Change this value as per your requirement

    generateRequest(CotractNumberCount);

    return 0;
}
