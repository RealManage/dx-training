# HOA-Specific Prompt Templates 🏘️

Ready-to-use prompts for common HOA management development tasks.

## 📋 Violation Management

### Violation Notice Generator
```
Create a C# (.NET 9) service that generates violation notices with:
- Template-based notice generation
- Violation type: [Landscaping/Parking/Architectural/Noise/Other]
- Severity levels: Warning, First Notice, Final Notice
- Include: Property address, violation details, fine amount, cure date
- Photo attachment support
- Email and print formats
- Compliance with state HOA laws
- Multi-language support (English/Spanish)
- 95% test coverage
```

### Violation Escalation System
```
Build a violation escalation workflow that:
- Tracks stages: Reported → Verified → 1st Notice → 2nd Notice → Final → Legal
- 30/60/90 day escalation periods
- Automatic fine increases: $50 → $100 → $200
- Board review triggers
- Appeal process integration
- Email notifications at each stage
- Audit trail for all actions
- Dashboard for violation trends
- Include xUnit tests for all scenarios
```

### Appeal Processing
```
Implement a violation appeal system with:
- Online appeal submission
- 30-day appeal window validation
- Required fields: Reason, evidence attachments
- Board review queue
- Voting mechanism for board members
- Decision notification system
- Appeal can only be submitted once
- Historical appeal tracking
- Test coverage including edge cases
```

## 💰 Financial Management

### Late Fee Calculator
```
Create a late fee calculation service that:
- Base parameters: Original amount, due date, grace period (30 days)
- 10% monthly compound interest after grace period
- Maximum cap at $1,000 or 50% of original (whichever is less)
- Handles partial payments
- Prorates for mid-month payments
- Generates payment schedules
- Includes waiver/adjustment capability
- Full test suite with edge cases
```

### Payment Processing
```
Build a payment processing module that:
- Accepts ACH (no fee) and credit cards (2.9% fee)
- Processes one-time and recurring payments
- Auto-pay enrollment
- Payment plans for large assessments
- NSF handling and retry logic
- Receipt generation (email/PDF)
- QuickBooks integration
- PCI DSS compliance
- 95% test coverage
```

### Assessment Generator
```
Create an assessment calculation system for:
- Regular monthly/quarterly/annual dues
- Special assessments (one-time or installments)
- Proration for new owners
- Budget-based calculations
- Reserve fund contributions
- Different rates by property type/size
- Bulk invoice generation
- Export to accounting system
- Include comprehensive tests
```

## 📊 Reporting Systems

### Board Meeting Reports
```
Generate automated board meeting reports including:
- Financial summary (income/expenses/reserves)
- Violation statistics by type
- Collection rates and delinquencies
- Maintenance completed/pending
- New resident welcomes
- Upcoming projects
- Format: PDF with charts (Chart.js)
- Email distribution to board
- Archive to document management
```

### Annual Budget Report
```
Create annual budget reporting that shows:
- Budget vs actual by category
- Variance analysis with explanations
- Reserve fund status
- 5-year projection
- Assessment recommendation
- Vendor spend analysis
- Charts and graphs
- Export to Excel
- Printer-friendly format
```

### Delinquency Report
```
Build a delinquency tracking system that:
- Lists all overdue accounts
- Aging buckets: 30/60/90/120+ days
- Total outstanding by property
- Payment history for each unit
- Collection status and actions taken
- Legal action triggers
- Export for collection agency
- Automated monthly generation
```

## 👥 Resident Management

### Welcome Package System
```
Create automated welcome package for new residents:
- Generate welcome letter
- Include: CCRs, rules summary, contact info
- Payment setup instructions
- Amenity access information
- Emergency contacts
- Architectural review process
- Digital delivery via email
- Physical package request option
- Track delivery confirmation
```

### Communication Portal
```
Build a resident communication system with:
- Announcement broadcasting
- Category filters: Maintenance, Events, Emergencies
- Email/SMS/App notifications
- Delivery confirmation tracking
- Language preferences
- Opt-in/opt-out management
- Template library
- Scheduled sending
- Analytics on open rates
```

### Directory Management
```
Implement a resident directory that:
- Maintains owner and tenant information
- Emergency contacts
- Vehicle registration
- Pet registration
- Privacy settings (opt-in sharing)
- Board member identification
- Property manager contacts
- Vendor contacts
- Export capabilities
- GDPR compliance
```

## 🔧 Maintenance Operations

### Work Order System
```
Create a maintenance work order system:
- Request submission (resident portal)
- Priority levels: Emergency/High/Normal/Low
- Auto-assignment to vendors
- Status tracking workflow
- Photo attachments
- Cost estimates and approvals
- Completion confirmation
- Resident satisfaction survey
- Reporting and analytics
- Mobile-friendly for field use
```

### Vendor Management
```
Build vendor management module with:
- Vendor database (licensed, insured)
- Service categories
- Performance ratings
- Contract tracking
- Insurance expiration alerts
- W-9 and compliance documents
- Invoice processing
- Bid collection system
- Preferred vendor flags
- Historical spend analysis
```

### Inspection Tracking
```
Implement property inspection system:
- Scheduled inspection calendar
- Inspection types: Annual, violation, architectural
- Checklist templates by type
- Photo documentation
- Violation auto-generation
- Historical inspection records
- Compliance reporting
- Inspector assignment
- Mobile app for field use
```

## 📅 Event Management

### Board Meeting Organizer
```
Create board meeting management system:
- Meeting scheduling and notifications
- Agenda builder from templates
- Document packet assembly
- Attendance tracking
- Minute recording assistant
- Action item tracking
- Executive session handling
- Virtual meeting integration
- Public comment period management
- Post-meeting distribution
```

### Community Events
```
Build community event management:
- Event calendar
- RSVP system
- Amenity reservations
- Event announcement distribution
- Volunteer coordination
- Budget tracking
- Photo gallery
- Feedback surveys
- Recurring event support
- Integration with communication system
```

## 📱 Mobile Solutions

### Resident Mobile App API
```
Create API endpoints for resident mobile app:
- Account balance and payment
- Violation status
- Document access
- Work order submission
- Announcement feed
- Directory lookup
- Amenity reservations
- Board contact
- Push notifications
- Offline capability
```

### Board Member Dashboard
```
Build board member dashboard showing:
- Financial snapshot
- Violation trends
- Occupancy rates
- Maintenance queue
- Upcoming meetings
- Action items
- Document library
- Quick approvals
- Mobile-responsive design
- Real-time updates
```

## 🔒 Compliance & Legal

### Lien Processing
```
Implement lien filing system:
- Automatic triggers at 90+ days
- State-specific requirements
- Notice generation
- Filing documentation
- Release upon payment
- Legal status tracking
- Attorney integration
- Cost recovery tracking
- Audit trail
- Compliance reporting
```

### Document Management
```
Create document management system for:
- CCRs and amendments
- Board resolutions
- Meeting minutes
- Financial statements
- Contracts
- Architectural approvals
- Version control
- Access permissions
- Retention policies
- Search capabilities
```

---

**Usage Tips:**
1. Replace placeholder values in brackets with your specific requirements
2. Add these templates to your CLAUDE.md for consistent results
3. Combine multiple templates for complex features
4. Always specify RealManage's 95% test coverage requirement